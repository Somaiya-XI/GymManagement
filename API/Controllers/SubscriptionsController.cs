using Application.Services;
using Application.Subscriptions.Commands.CreateSubscription;
using Application.Subscriptions.Commands.DeleteSubscription;
using Application.Subscriptions.Queries.GetSubscription;
using Contracts.Subscriptions;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = Domain.Subscriptions.SubscriptionType;
namespace API.Controllers;
[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ApiController
{
    private readonly ISender _mediator;

    public SubscriptionsController(ISender mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<ActionResult<ErrorOr<Guid>>> CreateSubscription(CreateSubscriptionRequest request)
    {

        if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(), out var subscriptionType))
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid subscription type");
        }
        
        var command = new CreateSubscriptionCommand(
            subscriptionType ,
            request.AdminId);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.MatchFirst(
            subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
            error => Problem());

        /* Similar to this logic is the function above: */

        // if (createSubscriptionResult.IsError)
        // {
        //     return Problem();
        // }
        // var response = new SubscriptionResponse(createSubscriptionResult.Value, request.SubscriptionType);
        //
        // return Ok(response);
    }

    [HttpGet("{subscriptionId:guid}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var query = new GetSubscriptionQuery(subscriptionId);

        var getSubscriptionsResult = await _mediator.Send(query);

        return getSubscriptionsResult.MatchFirst(
                    subscription => Ok(new SubscriptionResponse(
                        subscription.Id,
                        Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name))),
                    error => Problem());
    }

    [HttpDelete("{subscriptionId:guid}")]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        var command = new DeleteSubscriptionCommand(subscriptionId);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.Match(
            _ => NoContent(),
            Problem);
    }

}