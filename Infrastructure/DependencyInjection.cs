using Application.Common.Interfaces;
using Infrastructure.Common.Persistence;
using Infrastructure.Subscriptions.Persistence;
using Infrastructure.Gyms.Persistence;
using Infrastructure.Admins.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddPersistence();
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<GymManagementDbContext>(options =>
            options.UseSqlite("Data Source = gymManagement.db"));

        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<IGymRepository, GymRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymManagementDbContext>());

        return services;
    }
}