using Domain.Rooms;
using MediatR;
using ErrorOr;
namespace Application.Rooms.Commands.CreateRoom;

public record CreateRoomCommand( Guid GymId,
    string RoomName) : IRequest<ErrorOr<Room>>;