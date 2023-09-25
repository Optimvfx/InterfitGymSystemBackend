namespace BLL.Services.TimeService;

public class StandartBaseTimeService : BaseTimeService
{
    protected override TimeSpan GetPassedTime(DateTime lastRequestTime)
    {
        return DateTime.UtcNow - lastRequestTime;
    }

    protected override DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}