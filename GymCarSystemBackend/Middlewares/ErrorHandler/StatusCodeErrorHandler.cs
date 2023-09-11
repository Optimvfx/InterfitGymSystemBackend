using System.Net;

namespace GymCarSystemBackend.Middlewares.ErrorHandler;

public class StatusCodeErrorHandler : IErrorHandler
{
    private readonly HttpStatusCode _statusCode;

    public StatusCodeErrorHandler(HttpStatusCode statusCode)
    {
        _statusCode = statusCode;
    }

    public void Handle(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)_statusCode;
    }
}