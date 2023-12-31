﻿using System.Net;
using GymCardSystemBackend.Extensions;
using GymCardSystemBackend.Middlewares.ErrorHandler;
using GymCardSystemBackend.Middlewares.ErrorHandler.PossibleErrorHandler;

namespace GymCardSystemBackend.DependencyInjection;

public static class DependencyInjectionMiddleware
{
    private static IPossibleErrorHandler[] _possibleErrorHandlers => new []
    {
        new ErrorHandlerByExceptionType(typeof(ArgumentNullException), HttpStatusCode.BadRequest),
        new ErrorHandlerByExceptionType(typeof(ArgumentException), HttpStatusCode.BadRequest),
        new ErrorHandlerByExceptionType(typeof(UnauthorizedAccessException), HttpStatusCode.Unauthorized),
        new ErrorHandlerByExceptionType(typeof(FileNotFoundException), HttpStatusCode.NotFound),
        new ErrorHandlerByExceptionType(typeof(DirectoryNotFoundException), HttpStatusCode.NotFound),
        new ErrorHandlerByExceptionType(typeof(NotSupportedException), HttpStatusCode.MethodNotAllowed),
        new ErrorHandlerByExceptionType(typeof(InvalidOperationException), HttpStatusCode.Conflict),
        new ErrorHandlerByExceptionType(typeof(TimeoutException), HttpStatusCode.RequestTimeout),
        new ErrorHandlerByExceptionType(typeof(WebException), HttpStatusCode.InternalServerError),
    };
    
    public static IApplicationBuilder AddMiddlewares(this IApplicationBuilder app, LogLevel logLevel)
    {
        app.UseErrorHandlerMiddleware(
            new StatusCodeErrorHandler(HttpStatusCode.InternalServerError), logLevel, _possibleErrorHandlers);

        return app;
    }
}