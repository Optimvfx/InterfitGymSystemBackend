namespace Common.Extensions;

public static class IEnumerableExtensions
{
    public static bool Nothing<T>(this IEnumerable<T> enumerable)
    {
        var any = enumerable.Any();
        return any == false;
    }
    
    public static bool Nothing<T>(this IEnumerable<T> enumerable, Func<T, bool> validValue)
    {
        var any = enumerable.Any(e => validValue.Invoke(e));
        return any == false;
    }
}