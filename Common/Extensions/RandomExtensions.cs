using System.Text;
using Common.Models;

namespace Common.Extensions;

public static class RandomExtensions
{
    public static uint NextUint(this Random rand, uint max)
    {
        return (uint)rand.Next((int)max);
    }
    
    public static bool NextBool(this Random rand, Procent? trueProcent = null)
    {
        var procent = trueProcent ?? Procent.Half;
        return rand.NextDouble() < procent.Value;
    }

    public static DateTime NextDate(this Random rand, ValueRange<DateTime> range)
    {
        var rangeDiapasone = range.Max - range.Min;
        var randomRangeDiapasone = rangeDiapasone * rand.NextDouble();

        return range.Min + rangeDiapasone;
    }
    
    public static string NextEngAlphaviteString(this Random random, int length, Procent? emptyProcent = null)
    {
        //Use already generated char list for optimization 
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        
        emptyProcent = emptyProcent ?? new Procent(Procent.Min);
        
        if (length < 0)
            throw new AggregateException();
        
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            if (random.NextBool(emptyProcent))
            {
                stringBuilder.Append(' ');
                continue;
            }
            
            int index = random.Next(Chars.Length);
            char randomChar = Chars[index];
            stringBuilder.Append(randomChar);
        }

        return stringBuilder.ToString();
    }
}