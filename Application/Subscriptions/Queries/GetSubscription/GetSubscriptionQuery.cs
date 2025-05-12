using Domain.Subscriptions;
using MediatR;
using ErrorOr;

namespace Application.Subscriptions.Queries.GetSubscription;


public record GetSubscriptionQuery(Guid SubscriptionId)
    : IRequest<ErrorOr<Subscription>>;