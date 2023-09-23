namespace GymCardSystemBackend.DependencyInjection;

public static class DependencyInjectionLogging
{
    public static ILoggingBuilder AddCustomLogging(this ILoggingBuilder builder, LogLevel logLevel = LogLevel.Trace)
    {
        builder.ClearProviders();
        builder.AddConsole();

        return builder;
    }
}