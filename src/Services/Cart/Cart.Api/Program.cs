using MessageBus.Core.Configurations;
using MessageBus.Core.Extensions;
using static Catalog.Grpc.Protos.ProductGrpcService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqSetting>(builder.Configuration.GetSection(nameof(RabbitMqSetting)));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisSetting:ConnectionString"];
});

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddGrpcClient<ProductGrpcServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSetting:CatalogUrl"]);
});

builder.Services.AddMassTransitWithRabbitMq();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
