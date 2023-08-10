using MessageBus.Core.Configurations;
using MessageBus.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Order.Api.Contracts;
using Order.Application.Contracts;
using Order.Application.Extensions;
using Order.Infrastructure.Data;
using Order.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqSetting>(builder.Configuration.GetSection(nameof(RabbitMqSetting)));

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUserProvider<string>, CurrentUserProvider>();

builder.Services.AddControllers();

builder.Services.AddMassTransitWithRabbitMq();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
