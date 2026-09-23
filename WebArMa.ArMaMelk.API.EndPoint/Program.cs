using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebArMa.ArMaMelk.API.Application._Shared.Contexts;
using WebArMa.ArMaMelk.API.Application.Auth.Services;
using WebArMa.ArMaMelk.API.Application.Redis;
using WebArMa.ArMaMelk.API.EndPoint.Utils.Configurations;
using WebArMa.ArMaMelk.Persistence.Redis.Services;
using WebArMa.ArMaMelk.Persistence.Seeds;
using WebArMa.ArMaMelk.Persistence.SQL.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediator(option => option.ServiceLifetime = ServiceLifetime.Transient);

builder.Services.AddControllers();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"),
        npgsqlOptions =>
        {
            npgsqlOptions.UseNetTopologySuite();
        });
});

builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();

builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddApiVersioningConfig();
builder.Services.AddTypeAdapterConfig();

builder.Services.AddScoped<ProvinceSeeder>();
builder.Services.AddScoped<CitySeeder>();
builder.Services.AddScoped<CountySeeder>();
builder.Services.AddScoped<VillageSeeder>();
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var provinceSeeder = services.GetRequiredService<ProvinceSeeder>();
    var citySeeder = services.GetRequiredService<CitySeeder>();
    var countySeeder = services.GetRequiredService<CountySeeder>();
    var villageSeeder = services.GetRequiredService<VillageSeeder>();

    await provinceSeeder.SeedAsync();
    await citySeeder.SeedAsync();
    await countySeeder.SeedAsync();
    await villageSeeder.SeedAsync();
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
