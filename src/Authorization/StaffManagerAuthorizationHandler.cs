using System.Threading.Tasks;
using src.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace src.Authorization
{
    public class StaffManagerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Staff>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Staff resource)
        {
            if(context.User == null || resource == null)
            {
                return Task.FromResult(0);
            }

            //If not asking for approval/reject, return.
            if(requirement.Name != Constants.ReadOperationName &&
            requirement.Name != Constants.UpdateOperationName &&
            requirement.Name != Constants.DeleteOperationName)
            {
                return Task.FromResult(0);
            }

            //Managers can read, update or delete staff
            if(context.User.IsInRole(Constants.BusCompanyManagersRole))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}