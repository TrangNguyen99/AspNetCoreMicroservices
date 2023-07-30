using Catalog.Api.Configurations;
using Catalog.Api.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogDbContext
    {
        public IMongoCollection<Product> Products { get; set; }

        public CatalogDbContext(IOptions<MongoDbSetting> mongoDbSetting)
        {
            Console.WriteLine("mongoDbSetting.Value.ConnectionString");
            Console.WriteLine(mongoDbSetting.Value.ConnectionString);
            var client = new MongoClient(mongoDbSetting.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSetting.Value.DatabaseName);
            Products = database.GetCollection<Product>(nameof(Products));
        }
    }
}
