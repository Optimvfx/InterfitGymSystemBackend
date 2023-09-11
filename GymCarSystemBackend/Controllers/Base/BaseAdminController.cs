using BLL.Services.DataCoder;
using CLL.Consts;
using CLL.ControllersLogic;
using Common.Convertors;
using Common.Exceptions.User;
using Microsoft.AspNetCore.Mvc;

namespace GymCarSystemBackend.Controllers.Base;

public abstract class BaseAdminController : BaseController
{
    private readonly AuthControllerLogic _authController;

    protected BaseAdminController(IDataCoder<Guid, string> guidCryptor, AuthControllerLogic authController) : base(guidCryptor)
    {
        _authController = authController;
    }

    protected async Task<Guid> GetAdminId()
    {
        var claims = User.Claims;
        
        if (claims.TryGetClaimValue(Claims.UserClaim, out Guid id))
        {
            if (await _authController.AnyAdminByAccess(id))
            {
                return await _authController.GetAdminByAccess(id);
            }
        }

        throw new InvalidUserIdException();
    }
}
