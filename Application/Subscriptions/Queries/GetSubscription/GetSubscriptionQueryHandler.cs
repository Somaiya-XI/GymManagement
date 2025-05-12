using Application.Common.Interfaces;
using Domain.Subscriptions;
using MediatR;
using ErrorOr;
namespace Application.Subscriptions.Queries.GetSubscription;

public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetSubscriptionQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery query, CancellationToken cancellationToken)
    {
        //Access the subscription with givenId
        var subscription = await _subscriptionRepository.GetByIdAsync(query.SubscriptionId);
        Console.WriteLine($"Subscription with id {query.SubscriptionId} was found: {subscription}");
        //return the subscription
        return subscription is null
            ? Error.NotFound(description: "Subscription with the given id was not found.")
            : subscription;
    }
}