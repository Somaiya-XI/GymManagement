using Application.Common.Interfaces;
using Domain.Gyms;
using MediatR;
using ErrorOr;
namespace Application.Gyms.Queries.GetGym;

public class GetGymQueryHandler: IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymRepository _gymsRepository;
    
    public GetGymQueryHandler(IGymRepository gymsRepository, ISubscriptionRepository subscriptionRepository)
    {
        _gymsRepository = gymsRepository;
        _subscriptionRepository = subscriptionRepository;
    }
    public async Task<ErrorOr<Gym>> Handle(GetGymQuery query, CancellationToken cancellationToken)
    {
        if (! await _subscriptionRepository.ExistsAsync(query.SubscriptionId))
        {
            return Error.NotFound(description: "Subscription not found");       
        }
        
        if (await _gymsRepository.GetByIdAsync(query.GymId) is not Gym gym)
        {
            return Error.NotFound(description: "Gym not found");
        }
        return gym;
    }
}