
using ErrorOr;
using MediatR;

namespace Application.Subscriptions.Commands.DeleteSubscription;

public record DeleteSubscriptionCommand(Guid SubscriptionId): IRequest<ErrorOr<Deleted>>;