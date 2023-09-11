using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GymCarSystemBackend.DependencyInjection;

public static class DependencyInjectionJwt
{
    private const string JwtKey = "JWTKEY";

    public static IServiceCollection AddJwtAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var key = GetSymmetricSecurityKey(configuration);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }
    
    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Тренажерна Зала Interfit API", Version = "v0.1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Заголовок авторизації з використанням схеми Bearer. Приклад: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });


        return services;
    }
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey(ConfigurationManager configuration)
    {
        var key = configuration[JwtKey];
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}