using System.Net;
using Common.Extensions;

namespace GymCardSystemBackend.Middlewares.ErrorHandler.PossibleErrorHandler;

public class ErrorHandlerByExceptionType : IPossibleErrorHandler
{
    private readonly Type _exceptionType;
    private readonly IErrorHandler _errorHandler;
    
    public ErrorHandlerByExceptionType(Type exceptionType, IErrorHandler errorHandler)
    {
        if (exceptionType.GetBaseType().IsNot(typeof(Exception)))
            throw new ArgumentException();
        
        _exceptionType = exceptionType;
        _errorHandler = errorHandler;
    }
    
    public ErrorHandlerByExceptionType(Type exceptionType, HttpStatusCode statusCode)
    {
        if (exceptionType.GetBaseType().IsNot(typeof(Exception)))
            throw new ArgumentException();
        
        _exceptionType = exceptionType;
        _errorHandler = new StatusCodeErrorHandler(statusCode);
    }

    public void Handle(HttpContext context, Exception exception)
    {
        _errorHandler.Handle(context, exception);
    }

    public bool CanHandle(Exception exception)
    {
        return _exceptionType.IsInstanceOfType(exception);
    }
}