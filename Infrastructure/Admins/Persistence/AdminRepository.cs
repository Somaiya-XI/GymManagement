using Application.Common.Interfaces;
using Domain.Admins;
using Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Admins.Persistence;


public class AdminRepository : IAdminRepository
{
    private readonly GymManagementDbContext _dbContext;

    public async Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return await _dbContext.Admins.FirstOrDefaultAsync(admin => admin.Id == adminId);
    }

    public Task UpdateAsync(Admin admin)
    {
        _dbContext.Update(admin);

        return Task.CompletedTask;
    }
}