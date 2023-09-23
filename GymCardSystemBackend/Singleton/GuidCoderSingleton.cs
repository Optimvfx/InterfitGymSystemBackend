using BLL.Services.DataCoder;

namespace GymCardSystemBackend.Singleton;

public static class GuidCoderSingleton
{
    private static IDataCoder<Guid, string> _guidCryptor;

    public static IDataCoder<Guid, string> GetGuidCoder()
    {
        if (_guidCryptor == null)
            throw new InvalidOperationException();
        
        return _guidCryptor;
    }

    public static void Init(IDataCoder<Guid, string> guidCryptor)
    {
        if (_guidCryptor != null)
            throw new InvalidOperationException();
        
        _guidCryptor = guidCryptor;
    }
}