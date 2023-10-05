using Common.Models;

namespace Common.Exceptions.General;

public class OutOfValueRangeException : ArgumentException
{
    private double Value;
    private double? Max;
    private double? Min;

    public override string Message => GetMessage();

   private OutOfValueRangeException()
    {
        
    }
    
    private OutOfValueRangeException(double value, ValueRange<double> range)
    {
        Value = value;
        Max = range.Max;
        Min = range.Min;
    }

    private string GetMessage()
    {
        if (Min == null)
            return $"Value {Value} was larger then {Max}.";

        if(Max == null)
            return $"Value {Value} was smaller then {Min}.";
        
        return $"Value {Value} was larger then {Max} or smaller then {Min} min.";
    }

    public static void ThrowIfLarger(double value, double max)
    {
        if (value > max)
            throw new OutOfValueRangeException()
            {
                Value = value,
                Max = max
            };
    }
    
    public static void ThrowIfOutOfRange(double value, ValueRange<double> range)
    {
        if (range.OutOfRange(value))
            throw new OutOfValueRangeException(value, range);
    }

    public static void ThrowIfSmaller(double value, double min)
    {
        if (value < min)
            throw new OutOfValueRangeException()
            {
                Value = value,
                Min = min
            };
    }
}