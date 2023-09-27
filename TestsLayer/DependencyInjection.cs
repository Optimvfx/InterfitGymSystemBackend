using Microsoft.Extensions.DependencyInjection;

namespace TestsLayer;

public static class DependencyInjection
{
    private static IServiceCollection _serviceCollection => DependencyInjectionSingleton.GetTestServices();
    
    public static T Get<T>()
    {
        using(_serviceCollection.P)
    }
}     