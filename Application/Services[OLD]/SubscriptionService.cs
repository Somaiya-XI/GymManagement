namespace Application.Services;

public class SubscriptionService : ISubscriptionWriteService
{
    public Guid CreateSubscription(string subscriptionType, Guid adminId)
    {
        return Guid.NewGuid();
    }
}