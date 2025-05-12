using ErrorOr;
using MediatR;

namespace Application.Gyms.Commands.DeleteGym;

public record DeleteGymCommand(Guid SubscriptionId, Guid GymId): IRequest<ErrorOr<Deleted>>;