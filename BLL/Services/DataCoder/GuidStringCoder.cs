using Common.Models;

namespace BLL.Services.DataCoder;

public class GuidStringCoder : IDataCoder<Guid, string>
{
    private readonly IDataCoder<byte[], byte[]>? _byteCoder;
    private readonly IDataCoder<byte[], string> _stringCoder;

    public GuidStringCoder(IDataCoder<byte[], string> stringCoder, IDataCoder<byte[], byte[]>? byteCoder = null)
    {
        _byteCoder = byteCoder;
        _stringCoder = stringCoder;
    }

    public string Encrypt(Guid value)
    {
        var guidBytes = value.ToByteArray();

        if (_byteCoder == null)
            return _stringCoder.Encrypt(guidBytes);

        var codedGuidBytes = _byteCoder.Encrypt(guidBytes);
        return _stringCoder.Encrypt(codedGuidBytes);
    }

    public Guid Decrypt(string coded)
    {
        var decimalBytes = _stringCoder.Decrypt(coded);

        if (_byteCoder == null)
            return new Guid(decimalBytes);

        var decodedDecimalBytes = _byteCoder.Decrypt(decimalBytes);
        return new Guid(decodedDecimalBytes);
    }

    public Result<Guid> TryDecrypt(string coded)
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
}