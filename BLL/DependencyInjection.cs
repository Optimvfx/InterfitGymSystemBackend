using BLL.AutoMapper.Profiles;
using BLL.Services._Access;
using Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, 
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.AddWithCustomLifeTime<AuthService>(serviceLifetime);
        services.AddWithCustomLifeTime<AdminService>(serviceLifetime);
        
        services.AddMapper();
        
        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BaseAutoMapperProfile).Assembly);
        return services;
    }
}
