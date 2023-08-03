using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Core.Data;

public class CatalogDbContext
{
    public IMongoCollection<Product> Products { get; set; }

    public CatalogDbContext(IMongoDatabase database)
    {
        Products = database.GetCollection<Product>(nameof(Products));
    }
}
