namespace GymCardSystemBackend.Middlewares.ErrorHandler;

public interface IErrorHandler
{
    void Handle(HttpContext context, Exception exception);
}