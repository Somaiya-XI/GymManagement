using Domain.Gyms;

namespace Application.Common.Interfaces;

public interface IGymRepository
{
    public Task<Gym?> GetByIdAsync(Guid gymId);
    public Task AddGymAsync(Gym gym);
    public Task RemoveGymAsync(Gym gym);
    public Task<bool> ExistsAsync(Guid id);
    public Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
    public Task UpdateGymAsync(Gym gym);
    public Task RemoveRangeAsync(List<Gym> gyms);


}