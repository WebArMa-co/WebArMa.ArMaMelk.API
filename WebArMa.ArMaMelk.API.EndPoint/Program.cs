using MapsterMapper;
using StackExchange.Redis;
using WebArMa.ArMaMelk.API.Application.Redis;
using WebArMa.ArMaMelk.API.EndPoint.Utils.Configurations;
using WebArMa.ArMaMelk.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));

builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddApiVersioningConfig();
builder.Services.AddTypeAdapterConfig();

builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddScoped<IRedisService, RedisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
