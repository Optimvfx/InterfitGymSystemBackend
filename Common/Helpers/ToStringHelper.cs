using System.Text;
using Microsoft.Extensions.Primitives;

namespace Common.Helpers;

public class ToStringHelper
{
    public static string ToString(byte[] bytes)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < bytes.Length - 1; i++)
        {
            sb.Append(bytes[i] + "-");
        }

        sb.Append(bytes.Last());
        
        return sb.ToString();
    }
}