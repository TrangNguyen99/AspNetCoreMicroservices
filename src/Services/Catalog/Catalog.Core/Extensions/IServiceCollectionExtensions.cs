using Catalog.Core.Configurations;
using Catalog.Core.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Core.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCatalogDbContext(this IServiceCollection services)
    {
        services.AddScoped((serviceProvider) =>
        {
            var mongoDbSetting = serviceProvider.GetRequiredService<IOptions<MongoDbSetting>>();
            var mongoClient = new MongoClient(mongoDbSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSetting.Value.DatabaseName);

            return new CatalogDbContext(mongoDatabase);
        });

        return services;
    }
}
