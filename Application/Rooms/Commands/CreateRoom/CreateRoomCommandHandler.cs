using Application.Common.Interfaces;
using Domain.Rooms;
using ErrorOr;
using MediatR;

namespace Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler: IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
       var gym  = await _gymRepository.GetByIdAsync(command.GymId);
       
       if (gym is null)
       {
           return Error.NotFound(description: "Gym not found");
       }
       
       var subscription = await _subscriptionRepository.GetByIdAsync(gym.SubscriptionId);
       
       if (subscription is null)
       {
           return Error.NotFound(description: "Subscription not found");      
       }

       Room room = new Room(
           command.RoomName,
           gym.Id,
           subscription.GetMaxDailySessions());
       
       var addRoomResult = gym.AddRoom(room);

       if (addRoomResult.IsError)
       {
           return addRoomResult.Errors;
       }
       
       await _gymRepository.UpdateGymAsync(gym);
       await _unitOfWork.CommitChangesAsync();
       
       return room;
    }
}