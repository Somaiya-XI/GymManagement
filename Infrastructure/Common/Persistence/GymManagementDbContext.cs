using System.Reflection;
using Application.Common.Interfaces;
using Domain.Admins;
using Domain.Gyms;
using Domain.Subscriptions;

namespace Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
public class GymManagementDbContext: DbContext, IUnitOfWork
{
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Gym> Gyms { get; set; } = null!;
    
    public GymManagementDbContext(DbContextOptions<GymManagementDbContext> options) : base(options)
    {
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}