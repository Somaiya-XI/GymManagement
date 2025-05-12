using Domain.Gyms;
using MediatR;
using ErrorOr;
namespace Application.Gyms.Queries.GetGym;

public record GetGymQuery(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Gym>>;