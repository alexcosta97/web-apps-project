using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;
using src.Models;

namespace src.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public RoleUsersTagHelper(UserManager<ApplicationUser> usrmgr, RoleManager<IdentityRole> rmgr)
        {
            userManager = usrmgr;
            roleManager = rmgr;
        }

        [HtmlAttributeName("identity-role")]
        public string Role{get;set;}

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            IdentityRole role = await roleManager.FindByIdAsync(Role);
            if(role != null)
            {
                foreach(var user in userManager.Users)
                {
                    if(userManager !=  null && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
    }
}