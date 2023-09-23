using CLL.Consts;
using CLL.ControllersLogic.Interface.AccessLogic;
using Common.Convertors;
using Microsoft.AspNetCore.Authorization;

namespace GymCardSystemBackend.AuthorizationHandlers;

public class AdminAuthorizationHandler : AuthorizationHandler<AdminRoleRequirement>
{
    private readonly IAuthLogic _authLogic;

    public AdminAuthorizationHandler(IAuthLogic authLogic)
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
                if (await _authLogic.AccessIsAdmin(id))
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