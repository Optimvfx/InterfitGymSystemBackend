using Common.Models;
using System.Security.Cryptography;

namespace BLL.Services.DataCoder;

public class ByteDataCoder : IDataCoder<byte[], byte[]>, IDisposable
{
    private readonly Aes _aesCoder;

    public ByteDataCoder(byte[] key)
    {
        const int AesKeyLenght = 16;
        
        if (key.Length != AesKeyLenght)
            throw new AggregateException();
        
        _aesCoder = Aes.Create();
        _aesCoder.Key = key;
        _aesCoder.Mode = CipherMode.CFB;
    }

    public byte[] Encrypt(byte[] value)
    {
        using (ICryptoTransform encryptor = _aesCoder.CreateEncryptor(_aesCoder.Key, _aesCoder.IV))
        {
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(value, 0, value.Length);
                    csEncrypt.FlushFinalBlock();
                }

                return msEncrypt.ToArray();
            }
        }
    }


    public byte[] Decrypt(byte[] coded)
    {
        using (ICryptoTransform decryptor = _aesCoder.CreateDecryptor(_aesCoder.Key, _aesCoder.IV))
        {
            using (MemoryStream msDecrypt = new MemoryStream(coded))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (MemoryStream msDecrypted = new MemoryStream())
                    {
                        csDecrypt.CopyTo(msDecrypted);
                        return msDecrypted.ToArray();
                    }
                }
            }
        }
    }

    public Result<byte[]> TryDecrypt(byte[] coded)
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

    public void Dispose()
    {
        _aesCoder.Dispose();
    }
}