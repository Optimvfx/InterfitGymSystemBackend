using BLL.AutoMapper.Profiles;
using BLL.Models.Employee;
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
        services.AddWithCustomLifeTime<AdminService>(serviceLifetime);
        
        services.AddWithCustomLifeTime<EmployeeService>(serviceLifetime);
        services.AddWithCustomLifeTime<PositionService>(serviceLifetime);
        services.AddWithCustomLifeTime<TimetableService>(serviceLifetime);
        services.AddWithCustomLifeTime<VacationService>(serviceLifetime);
        
        services.AddMapper();
        
        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BaseAutoMapperProfile).Assembly);
        return services;
    }
}
