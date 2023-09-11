namespace GymCarSystemBackend.Middlewares.ErrorHandler;

public class ErrorHandlerCollection
{
    private readonly List<IPossibleErrorHandler> _errorHandlers = new List<IPossibleErrorHandler>();
    private readonly IErrorHandler _standartErrorHandler;
        
    public ErrorHandlerCollection(IEnumerable<IPossibleErrorHandler> errorHandlers,IErrorHandler standart) : this(standart)
    {
        foreach (var errorHandler in errorHandlers)
        {
            AddErrorHandler(errorHandler);
        }
    }

    public ErrorHandlerCollection(IErrorHandler standart)
    {
        _standartErrorHandler = standart;
    }

    public void Handle(HttpContext context, Exception exception)
    {
        if (_errorHandlers.Any(h => h.CanHandle(exception)))
        {
            var handler = _errorHandlers.First(h => h.CanHandle(exception));
                
            handler.Handle(context, exception);
            return;
        }

        _standartErrorHandler.Handle(context, exception);
    }
        
    public void AddErrorHandler(IPossibleErrorHandler errorHandler)
    {
        _errorHandlers.Add(errorHandler);
    }
}
    
public interface IPossibleErrorHandler : IErrorHandler
{
    bool CanHandle(Exception exception);
}
    
public interface IErrorHandler
{
    void Handle(HttpContext context, Exception exception);
}