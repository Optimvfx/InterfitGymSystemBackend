namespace BLL.Services.TimeService;

public abstract class BaseTimeService
{
    private DateTime LastRequestTime;

    public BaseTimeService()
    {
        LastRequestTime = GetCurrentTime();
    }
    
    public DateTime GetCurrentDateTime()
    {
        var passedTime = GetPassedTime(LastRequestTime);

        if (passedTime.Ticks < 0)
            throw new ArgumentException("Negative passed time is not possible.");

        LastRequestTime = LastRequestTime.Add(passedTime);

        return LastRequestTime;
    }

    protected abstract TimeSpan GetPassedTime(DateTime lastRequestTime);
    protected abstract DateTime GetCurrentTime();
}