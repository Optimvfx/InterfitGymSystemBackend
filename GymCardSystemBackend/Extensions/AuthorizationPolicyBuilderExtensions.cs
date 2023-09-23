using GymCardSystemBackend.AuthorizationHandlers;
using Microsoft.AspNetCore.Authorization;

namespace GymCardSystemBackend.Extensions;

public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder RequireAdminRole(this AuthorizationPolicyBuilder builder)
    {
        builder.Requirements.Add(new AdminRoleRequirement());
        return builder;
    }
}