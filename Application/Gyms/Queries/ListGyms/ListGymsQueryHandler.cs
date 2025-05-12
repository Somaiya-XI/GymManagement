using Application.Common.Interfaces;
using Domain.Gyms;
using MediatR;
using ErrorOr;
namespace Application.Gyms.Queries.ListGyms;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly IGymRepository _gymsRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public ListGymsQueryHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymsRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _gymsRepository = gymsRepository;
    }

    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
    {
        if (!await _subscriptionRepository.ExistsAsync(query.SubscriptionId))
        {
            return Error.NotFound(description: "Subscription not found");       
        }
        return await _gymsRepository.ListBySubscriptionIdAsync(query.SubscriptionId);
    }
}