using Common.Models;

namespace BLL.Services.DataCoder;

public interface IDataCoder<TValue, TCoded>
{
    TCoded Encrypt(TValue value);
    TValue Decrypt(TCoded coded);
    Result<TValue> TryDecrypt(TCoded coded);
}