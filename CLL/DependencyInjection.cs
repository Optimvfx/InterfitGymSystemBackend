using CLL.ControllersLogic;
using CLL.ControllersLogic.Interface;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Extensions;

namespace CLL;

public static class DependencyInjection
{
    public static IServiceCollection AddControllersLogic(this IServiceCollection
        services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.AddControllersLogicConfigs();
        
        services.AddWithCustomLifeTime<IAuthLogic, AuthLogic>(serviceLifetime);
        
        return services;
    }
    
    private static IServiceCollection AddControllersLogicConfigs(this IServiceCollection
        services)
    {
        return services;
    }
}
