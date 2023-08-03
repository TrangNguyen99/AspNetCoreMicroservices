using Catalog.Core.Configurations;
using Catalog.Core.Extensions;
using Catalog.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSetting>(builder.Configuration.GetSection(nameof(MongoDbSetting)));

builder.Services.AddCatalogDbContext();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<ProductService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
