using ErrorOr;
using Domain.Gyms;
using MediatR;

namespace Application.Gyms.Commands.CreateGym;

public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;