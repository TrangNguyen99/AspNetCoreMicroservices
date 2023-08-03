using Catalog.Core.Data;
using Catalog.Grpc.Protos;
using Grpc.Core;
using MongoDB.Driver;

namespace Catalog.Grpc.Services;

public class ProductService : ProductGrpcService.ProductGrpcServiceBase
{
    private readonly CatalogDbContext _catalogDbContext;

    public ProductService(CatalogDbContext catalogDbContext)
    {
        _catalogDbContext = catalogDbContext;
    }

    public override async Task<GetProductsReply> GetProducts(GetProductsRequest request, ServerCallContext context)
    {
        var products = await _catalogDbContext.Products.Find(x => request.Ids.Contains(x.Id)).ToListAsync();

        var reply = new GetProductsReply();
        reply.Products.AddRange(products.Select(x => new Product
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price
        }));

        return reply;
    }
}
