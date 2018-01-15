using System.Threading.Tasks;
using src.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace src.Authorization
{
    public class AdministratorsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Staff>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Staff resource)
        {
            if(context.User == null)
            {
                return Task.FromResult(0);
            }

            //Administrators can do anything
            if(context.User.IsInRole(Constants.BusCompanyAdministratorsRole))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}