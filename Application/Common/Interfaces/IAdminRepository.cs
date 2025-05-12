using Domain.Admins;

namespace Application.Common.Interfaces;

public interface IAdminRepository
{
    public Task<Admin?> GetByIdAsync(Guid adminId);
    public Task UpdateAsync(Admin admin);
}