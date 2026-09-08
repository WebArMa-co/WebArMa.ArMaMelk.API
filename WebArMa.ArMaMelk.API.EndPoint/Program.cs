using Asp.Versioning;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebArMa.ArMaMelk.API.Application.Redis;
using WebArMa.ArMaMelk.API.Infrastructure.Mapster;
using WebArMa.ArMaMelk.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Key"]!)),

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWTConfig:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTConfig:Audience"],

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

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
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
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
