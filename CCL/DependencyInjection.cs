using BLL.AutoMapper.Profiles;
using BLL.Services;
using CCL.ControllersLogic;
using Common.Extensions;
using Common.Models;

namespace CCL;

public static class DependencyInjection
{
    public static IServiceCollection AddControllersLogic(this IServiceCollection
        services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.AddControllersLogicConfigs();
        
        services.AddWithCustomLifeTime<AuthControllerLogic>(serviceLifetime);

        return services;
    }
    
    private static IServiceCollection AddControllersLogicConfigs(this IServiceCollection
        services)
    {
        return services;
    }
}
