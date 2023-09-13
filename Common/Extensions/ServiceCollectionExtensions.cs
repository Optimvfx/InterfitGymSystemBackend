using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWithCustomLifeTime<T>(this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime , Func<IServiceProvider, T> getValue)
    where T : class
    {
        if (serviceLifetime == ServiceLifetime.Singleton)
            serviceCollection.AddSingleton<T>((s) => getValue.Invoke(s));
        else if (serviceLifetime == ServiceLifetime.Scoped)
            serviceCollection.AddScoped<T>((s) => getValue.Invoke(s));
        else if (serviceLifetime == ServiceLifetime.Transient)
            serviceCollection.AddTransient<T>((s) => getValue.Invoke(s));

        return serviceCollection;
    }
    
    public static IServiceCollection AddWithCustomLifeTime<T>(this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime)
        where T : class
    {
        if (serviceLifetime == ServiceLifetime.Singleton)
            serviceCollection.AddSingleton<T>();
        else if (serviceLifetime == ServiceLifetime.Scoped)
            serviceCollection.AddScoped<T>();
        else if (serviceLifetime == ServiceLifetime.Transient)
            serviceCollection.AddTransient<T>();

        return serviceCollection;
    }

    public static IServiceCollection AddWithCustomLifeTime<TBase, TValue>(this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime)
        where TBase : class
        where TValue : class, TBase

    {
        if (serviceLifetime == ServiceLifetime.Singleton)
            serviceCollection.AddSingleton<TBase, TValue>();
        else if (serviceLifetime == ServiceLifetime.Scoped)
            serviceCollection.AddScoped<TBase, TValue>();
        else if (serviceLifetime == ServiceLifetime.Transient)
            serviceCollection.AddTransient<TBase, TValue>();

        return serviceCollection;
    }
}