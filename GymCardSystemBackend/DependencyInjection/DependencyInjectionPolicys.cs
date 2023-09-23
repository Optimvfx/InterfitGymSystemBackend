using GymCardSystemBackend.AuthorizationHandlers;
using GymCardSystemBackend.Consts;
using GymCardSystemBackend.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GymCardSystemBackend.DependencyInjection;

public static class DependencyInjectionPolicys
{
    public static IServiceCollection AddAuthorizationPolicys(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationHandler, AdminAuthorizationHandler>();
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyConsts.AdminPolicy, policy =>
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