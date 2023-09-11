using Microsoft.AspNetCore.RateLimiting;

namespace GymCarSystemBackend.Extensions;

public static class RateLimiterOptionsCustomExtensions
{
    public static RateLimiterOptions AddSimpleFixedWindowLimiter(this RateLimiterOptions options, string name, uint permitLimit, uint timeLimitMinutes)
    {
        options.AddFixedWindowLimiter(name, (opt) =>
        {
            opt.PermitLimit = (int)permitLimit;
            opt.Window = TimeSpan.FromMinutes(timeLimitMinutes);
        });

        return options;
    }
}