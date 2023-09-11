using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GymCarSystemBackend.DependencyInjection;

public static class DependencyInjectionLogging
{
    public static ILoggingBuilder AddCustomLogging(this ILoggingBuilder builder, LogLevel logLevel = LogLevel.Trace)
    {
        builder.ClearProviders();
        builder.AddConsole();

        return builder;
    }
}