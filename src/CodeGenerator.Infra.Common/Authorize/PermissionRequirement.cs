using Microsoft.AspNetCore.Authorization;

namespace CodeGenerator.Infra.Common.Authorize
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        //Add any custom requirement properties if you have them
        public PermissionRequirement() { }
    }
}
