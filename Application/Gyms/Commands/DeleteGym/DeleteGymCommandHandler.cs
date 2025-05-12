using Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace Application.Gyms.Commands.DeleteGym;

public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymCommandHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand command, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetByIdAsync(command.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }
        var subscription = await _subscriptionRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        if (!subscription.HasGym(command.GymId))
        {
            return Error.NotFound(description: "Gym not found");       
        }
        
        subscription.RemoveGym(command.GymId);
        await _subscriptionRepository.UpdateAsync(subscription);
        await _gymRepository.RemoveGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();
        
        return Result.Deleted;
    }
}