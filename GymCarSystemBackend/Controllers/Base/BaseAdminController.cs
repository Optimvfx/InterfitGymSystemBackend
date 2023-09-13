using BLL.Services.DataCoder;
using CLL.Consts;
using CLL.ControllersLogic;
using CLL.ControllersLogic.Interface;
using Common.Convertors;
using Common.Exceptions.User;
using Microsoft.AspNetCore.Mvc;

namespace GymCarSystemBackend.Controllers.Base;

public abstract class BaseAdminController : BaseController
{
    private readonly IAuthLogic _auth;

    protected BaseAdminController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth) : base(guidCryptor)
    {
        _auth = auth;
    }

    protected async Task<Guid> GetAdminId()
    {
        var claims = User.Claims;
        
        if (claims.TryGetClaimValue(Claims.UserClaim, out Guid id))
        {
            var result = await _auth.TryGetAdminIdByAccess(id);
            if (result)
                return result.Value;
        }

        throw new InvalidUserIdException();
    }
}
