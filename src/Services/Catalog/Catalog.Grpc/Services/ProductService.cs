using Catalog.Grpc.Protos;
using Grpc.Core;

namespace Catalog.Grpc.Services
{
    public class ProductService : ProductGrpcService.ProductGrpcServiceBase
    {
        public override Task<GetProductsReply> GetProducts(GetProductsRequest request, ServerCallContext context)
        {
            return base.GetProducts(request, context);
        }
    }
}
