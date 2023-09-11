using GymCarSystemBackend.Middlewares;
using GymCarSystemBackend.Middlewares.ErrorHandler;

namespace GymCarSystemBackend.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder, 
        IErrorHandler standart, LogLevel logLevel, params IPossibleErrorHandler[] errorHandlers) 
    {
        var errorHandlerCollection = new ErrorHandlerCollection(errorHandlers, standart);
        builder.UseMiddleware<ErrorHandlerMiddleware>(errorHandlerCollection, logLevel);
        
        return builder;
    }
}