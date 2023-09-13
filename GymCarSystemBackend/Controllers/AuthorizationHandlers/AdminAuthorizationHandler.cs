using BLL.Services;
using CLL.Consts;
using CLL.ControllersLogic;
using Common.Convertors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GymCarSystemBackend.Controllers.AuthorizationHandlers;

public class AdminAuthorizationHandler : AuthorizationHandler<AdminRoleRequirement>
{
    private readonly AuthLogic _authLogic;

    public AdminAuthorizationHandler(AuthLogic authLogic)
    {
        _authLogic = authLogic;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminRoleRequirement requirement)
    {
        try
        {
            var user = context.User;

            if (user.Claims.TryGetClaimValue(Claims.UserClaim, out Guid id))
            {
                if (await _authLogic.TryGetAdminIdByAccess(id))
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail();
        }
        catch
        {
            context.Fail();
        }
    }
}

public class AdminRoleRequirement : IAuthorizationRequirement
{
}