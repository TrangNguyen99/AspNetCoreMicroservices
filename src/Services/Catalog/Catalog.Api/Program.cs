using Catalog.Api.Configurations;
using Catalog.Core.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSetting>(builder.Configuration.GetSection(nameof(MongoDbSetting)));

builder.Services.AddScoped((serviceProvider) =>
{
    var mongoDbSetting = serviceProvider.GetRequiredService<IOptions<MongoDbSetting>>();
    var mongoClient = new MongoClient(mongoDbSetting.Value.ConnectionString);
    var mongoDatabase = mongoClient.GetDatabase(mongoDbSetting.Value.DatabaseName);

    return new CatalogDbContext(mongoDatabase);
});

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

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
