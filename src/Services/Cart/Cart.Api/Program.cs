var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    Console.WriteLine("RedisSetting:ConnectionString");
    Console.WriteLine(builder.Configuration.GetValue<string>("RedisSetting:ConnectionString"));
    options.Configuration = builder.Configuration.GetValue<string>("RedisSetting:ConnectionString");
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
