using Common.Consts;
using Common.Models;

namespace BLL.Services.DataCoder;

public class ByteStringCoder : IDataCoder<byte[], string>
{
    private const int HalfByteMaxValue = 16;
    
    private readonly string[] _byteToString;
    private readonly IReadOnlyDictionary<string, byte> _stringToByte;

    public ByteStringCoder()
    {
        var firstParts = HexadecimalConsts.HexadecimalSymbols;
        var secondParts = HexadecimalConsts.HexadecimalSymbols;

        _byteToString = GenerateByteToString(firstParts, secondParts);
        _stringToByte = GenerateStringToByte(_byteToString);
    }

    public string Encrypt(byte[] value)
    {
        var stringsByBytes = value.Select(b => _byteToString[b]);

        return string.Join("", stringsByBytes);
    }

    public byte[] Decrypt(string coded)
    {
        const int CharsPerByte = 2;

        if (coded.Length % CharsPerByte != 0)
            throw new AggregateException();

        var bytes = new byte[coded.Length / CharsPerByte];
        
        for (int i = 0; i < coded.Length; i += CharsPerByte)
        {
            var subSting = coded.Substring(i, CharsPerByte);

            bytes[i / CharsPerByte] = _stringToByte[subSting];
        }

        return bytes;
    }

    public Result<byte[]> TryDecrypt(string coded)
    {
        try
        {
            return new(Decrypt(coded));
        }
        catch
        {
            return new();
        }
    }
    
    private IReadOnlyDictionary<string, byte> GenerateStringToByte(string[] byteToString)
    {
        if (byteToString.Length != HalfByteMaxValue * HalfByteMaxValue)
            throw new ArgumentException();

        var stringToByte = new Dictionary<string, byte>();
        
        for (int i = 0; i < byteToString.Length; i++)
        {
            stringToByte.Add(byteToString[i], (byte)i);
        }

        return stringToByte;
    }
    
    private string[] GenerateByteToString(char[] firstParts, char[] secondParts)
    {
        if (firstParts.Length != HalfByteMaxValue || secondParts.Length != HalfByteMaxValue)
            throw new AggregateException();

        var index = 0;

        var byteToString = new string[HalfByteMaxValue * HalfByteMaxValue];
        
        foreach (var firstPart in firstParts)
        {
            foreach (var secondPart in secondParts)
            {
                byteToString[index] = new string(new []{ firstPart, secondPart});
                index++;
            }
        }

        return byteToString;
    }
}