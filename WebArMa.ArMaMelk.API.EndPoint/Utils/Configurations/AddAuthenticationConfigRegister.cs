using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebArMa.ArMaMelk.API.Application.Redis;

namespace WebArMa.ArMaMelk.API.EndPoint.Utils.Configurations
{
    public static class AddAuthenticationConfigRegister
    {
        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfig:Key"]!)),

                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTConfig:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWTConfig:Audience"],

                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var redis = context.HttpContext.RequestServices.GetRequiredService<IRedisService>();
                        var principal = context.Principal!;

                        var jti = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                        var userId = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                        var tokenVersion = principal.FindFirst("token_version")?.Value;

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
            return services;
        }
    }
}
