using Domain.Subscriptions;
using MediatR;
using ErrorOr;
namespace Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId): IRequest<ErrorOr<Subscription>>;