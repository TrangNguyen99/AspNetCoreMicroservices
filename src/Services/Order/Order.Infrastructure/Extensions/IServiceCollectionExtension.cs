using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Repositories;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure.Extensions;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
        {
            // options.UseSqlServer(configuration.GetConnectionString("sqlserver"));
            options.UseNpgsql(configuration.GetConnectionString("postgresql"));
        });

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
