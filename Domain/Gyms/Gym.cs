using Domain.Rooms;
using ErrorOr;
using Throw;

namespace Domain.Gyms;

public class Gym
{
    private readonly int _maxRooms;
    private readonly List<Guid> _roomIds = new();
    private readonly List<Guid> _trainerIds = new();
    public Guid Id { get; }
    public string Name { get; }
    public Guid SubscriptionId { get; init; }

    public Gym(string name, int maxRooms, Guid subscriptionId, Guid? id = null)
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subscriptionId;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        // check if the list of roomIds already has this room
        _roomIds.Throw().IfContains(room.Id);
        
        // check if the rooms exceeding max rooms per subscription
        if (_roomIds.Count >= _maxRooms)
        {
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
        }
        // add the room to the gym
        _roomIds.Add(room.Id);
        return Result.Success;
    }

    public void RemoveRoom(Guid roomId)
    {
     _roomIds.Remove(roomId);   
    }
    
    public bool HasRoom(Guid roomId) => _roomIds.Contains(roomId);
    
    public ErrorOr<Success> AddTrainer(Guid trainerId)
    {
        if (_trainerIds.Contains(trainerId))
        {
            return Error.Conflict(description: "Trainer already added to gym");
        }
        _trainerIds.Add(trainerId);
        return Result.Success;
    }
    
    public bool HasTrainer(Guid trainerId) => _trainerIds.Contains(trainerId);
    
    private Gym(){}
}