namespace GymCardSystemBackend.Middlewares.ErrorHandler;

public interface IPossibleErrorHandler : IErrorHandler
{
    bool CanHandle(Exception exception);
}