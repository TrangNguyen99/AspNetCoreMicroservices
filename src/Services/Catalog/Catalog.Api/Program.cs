using Catalog.Core.Configurations;
using Catalog.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSetting>(builder.Configuration.GetSection(nameof(MongoDbSetting)));

builder.Services.AddCatalogDbContext();

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
