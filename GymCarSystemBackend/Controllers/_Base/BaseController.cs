using BLL.Services.DataCoder;
using Common.Extensions;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace GymCarSystemBackend.Controllers.Base;

public abstract class BaseController : Controller
{
    private IDataCoder<Guid, string> _guidCryptor;

    protected BaseController(IDataCoder<Guid, string> guidCryptor)
    {
        _guidCryptor = guidCryptor;
    }
    
    protected string EncryptGuid(Guid value) =>  _guidCryptor.Encrypt(value);
    protected Guid DecryptGuid(string coded) => _guidCryptor.Decrypt(coded);

    protected Result<Guid> TryDecryptGuid(string coded) => _guidCryptor.TryDecrypt(coded);
}