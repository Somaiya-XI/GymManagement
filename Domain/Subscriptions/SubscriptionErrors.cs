using ErrorOr;

namespace Domain.Subscriptions;

public static class SubscriptionErrors
{
    public static Error  CannotHaveMoreGymsThanSubscriptionAllows = Error.Validation(
        "Subscription.CannotHaveMoreGymsThanSubscriptionAllows",
        "A subscription cannot have more gyms than the subscription allows"); 
}