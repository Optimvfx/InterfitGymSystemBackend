using BLL;
using CLL;
using DAL;
using GymCardSystemBackend.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestsLayer;

public static class DependencyInjectionSingleton
{
    private static IServiceCollection? _serviceCollection = null;

    public static IServiceCollection GetTestServices()
    {
        if (_serviceCollection == null)
            _serviceCollection = GetServices();

        return _serviceCollection;
    }
    
    private static IServiceCollection GetServices()
    {
        IServiceCollection services = new ServiceCollection();
        var configuration = new ConfigurationManager();
        
        configuration.AddJsonFile(
            "C:\\Users\\maxim\\RiderProjects\\InterfitGymSystemBackend\\TestsLayer\\appsettings.Development.json");
        
        var connection = configuration.GetConnectionString("TestConnectionString");

        if (configuration == null)
            throw new FieldAccessException();
        
        services.AddMemoryCache();

        services.AddDatabase(connection, LogLevel.Debug);

        services.AddJwtAuth(configuration);
        services.AddSubServices(configuration);
        services.AddServices();
        services.AddControllersLogic();

        services.AddRateLimitPolicys();
        services.AddAuthorizationPolicys();
        services.AddControllers()
            .AddCustomJsonConvetors();

        services.AddEndpointsApiExplorer();
        services.AddCustomSwaggerGen();

        return services;
    }
}
