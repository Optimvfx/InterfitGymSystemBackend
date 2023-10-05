using BLL.Services.DataCoder;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Convertors;
using Common.Exceptions;
using DAL.Entities.Gym.Person.Employeers;
using GymCardSystemBackend.Consts;

namespace GymCardSystemBackend.Controllers._Base;

public class BaseTerminalController : BaseController
{
    private readonly IAuthLogic _auth;

    protected BaseTerminalController(IDataCoder<Guid, string> guidCryptor, IAuthLogic auth) : base(guidCryptor)
    {
        _auth = auth;
    }

    protected async Task<Guid> GetGymId()
    {
        var claims = User.Claims;
        
        if (claims.TryGetClaimValue(Claims.UserClaim, out Guid id))
        {
            var result = await _auth.TryGetGymIdByAccess(id);
            if (result)
                return result.Value;
            
            throw new InvalidUserIdException(typeof(TerminalAdministrator), id);
        }
        
        throw new InvalidUserIdException(typeof(TerminalAdministrator));
    }
}
