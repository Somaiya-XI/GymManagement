
using Application.Common.Interfaces;

namespace Application.Gyms.Commands.CreateGym;
using Domain.Gyms;
using ErrorOr;
using MediatR;

public class CreateGymCommandHandler: IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(command.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }
        Gym gym = new Gym(
            command.Name,
            subscription.GetMaxRooms(),
            command.SubscriptionId);
        
        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
        {
            return addGymResult.Errors;
        }
        await _gymsRepository.AddGymAsync(gym);
        await _subscriptionRepository.UpdateAsync(subscription);
        await _unitOfWork.CommitChangesAsync();
        return gym;
    }
}