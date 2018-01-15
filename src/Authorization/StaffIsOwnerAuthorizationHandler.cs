using System.Threading.Tasks;
using src.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace src.Authorization
{
    public class StaffIsOwnerAuthorizationHandler: AuthorizationHandler<OperationAuthorizationRequirement, Staff>
    {
        UserManager<ApplicationUser> _userManager;

        public StaffIsOwnerAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Staff resource)
        {
            if(context.User == null || resource == null)
            {
                return Task.FromResult(0);
            }

            //If we're not asking for CRUD permission return.

            if(requirement.Name != Constants.CreateOperationName &&
            requirement.Name != Constants.ReadOperationName &&
            requirement.Name != Constants.UpdateOperationName &&
            requirement.Name != Constants.DeleteOperationName)
            {
                return Task.FromResult(0);
            }

            if(resource.appUser.Equals(_userManager.GetUserAsync(context.User)))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}