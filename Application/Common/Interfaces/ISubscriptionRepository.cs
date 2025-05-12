using Domain.Subscriptions;
namespace Application.Common.Interfaces;

public interface ISubscriptionRepository
{
     Task AddSubscriptionAsync(Subscription subscription);
     Task<bool> ExistsAsync(Guid id);
     Task<Subscription?> GetByAdminIdAsync(Guid adminId);
     Task<Subscription?> GetByIdAsync(Guid subscriptionId);
     Task<List<Subscription>> ListAsync();
     public Task RemoveSubscriptionAsync(Subscription subscription);
     Task UpdateAsync(Subscription subscription);
}