using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebArMa.ArMaMelk.API.Application.Redis;
using WebArMa.ArMaMelk.API.Infrastructure.Mapster;
using WebArMa.ArMaMelk.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:key"]!)),

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWTConfig:issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTConfig:audience"],

        ValidateLifetime = true,

        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var redis = context.HttpContext.RequestServices.GetRequiredService<IRedisService>();
            var principal = context.Principal!;

            var jti = principal.FindFirstValue(JwtRegisteredClaimNames.Jti);
            var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var tokenVersion = principal.FindFirstValue("token_version");

            if (string.IsNullOrEmpty(jti) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(tokenVersion))
            {
                context.Fail("Invalid token claims.");
                return;
            }

            var revoked = await redis.ExistsAsync($"jwt:revoked:{jti}");

            if (revoked)
            {
                context.Fail("Token has been revoked.");
                return;
            }

            var currentVersion = await redis.GetAsync($"user:token-version:{userId}");

            if (currentVersion != tokenVersion)
            {
                context.Fail("Token version is invalid.");
                return;
            }
        }
    };
});

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(PersonMappingConfigurations).Assembly);
builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddScoped<IRedisService, RedisService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
