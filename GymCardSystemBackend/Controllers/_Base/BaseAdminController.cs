using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Convertors;
using Common.Exceptions.User;
using GymCardSystemBackend.Consts;

namespace GymCardSystemBackend.Controllers._Base;

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
            if (await _auth.AccessIsAdmin(id))
                return id;
        }

        throw new InvalidUserIdException();
    }
}
