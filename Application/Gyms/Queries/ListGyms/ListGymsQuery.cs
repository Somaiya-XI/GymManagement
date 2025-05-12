using Domain.Gyms;
using MediatR;
using ErrorOr;

namespace Application.Gyms.Queries.ListGyms;

public record ListGymsQuery(Guid SubscriptionId) : IRequest<ErrorOr<List<Gym>>>;