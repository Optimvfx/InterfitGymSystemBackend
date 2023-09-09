using BLL.AutoMapper.Profiles;
using BLL.Services;
using BLL.Services.ImageService;
using BLL.Services.TimeService;
using Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, 
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.AddWithCustomLifeTime<AuthService>(serviceLifetime);

        services.AddAutoMapper(typeof(BaseAutoMapperProfile).Assembly);
        services.AddMemoryCache();
        
        return services;
    }
}
