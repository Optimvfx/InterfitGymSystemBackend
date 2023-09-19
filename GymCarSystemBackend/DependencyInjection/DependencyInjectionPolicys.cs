using System.Net;
using BLL.Services.TokenService;
using CLL.Consts;
using GymCarSystemBackend.AuthorizationHandlers;
using GymCarSystemBackend.Extensions;
using GymCarSystemBackend.Middlewares.ErrorHandler;
using GymCarSystemBackend.Middlewares.ErrorHandler.PossibleErrorHandler;
using Microsoft.AspNetCore.Authorization;

namespace GymCarSystemBackend.DependencyInjection;

public static class DependencyInjectionPolicys
{
    public static IServiceCollection AddAuthorizationPolicys(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationHandler, AdminAuthorizationHandler>();
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireAdminRole();
            });
        });

        return services;
    }
    
    public static IServiceCollection AddRateLimitPolicys(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddSimpleFixedWindowLimiter("AuthenticateRateLimiting", 5, 5);
            options.AddSimpleFixedWindowLimiter("KeyCreationLimiting", 20, 60);
        });
        
        return services;
    }
}