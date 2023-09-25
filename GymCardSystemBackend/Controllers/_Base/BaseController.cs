using BLL.Services.DataCoder;
using Common.Models;
using Common.Models.PaginationView;
using Microsoft.AspNetCore.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace GymCardSystemBackend.Controllers._Base;

public abstract class BaseController : Controller
{
    private IDataCoder<Guid, string> _guidCryptor;

    protected BaseController(IDataCoder<Guid, string> guidCryptor)
    {
        _guidCryptor = guidCryptor;
    }

    protected IActionResult PaginationView<T>(BasePaginationView<T> paginationView, uint? page)
    {
        if (page == null)
            return Ok(paginationView.GetPagesRange());

        if (paginationView.PageOutOfRange(page.Value))
            return NotFound("Page out of range.");

        return Ok(paginationView.Get(page.Value));
    }
    
    protected string EncryptGuid(Guid value) =>  _guidCryptor.Encrypt(value);
    protected Guid DecryptGuid(string coded) => _guidCryptor.Decrypt(coded);

    protected Result<Guid> TryDecryptGuid(string coded) => _guidCryptor.TryDecrypt(coded);
}