using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Models;
using System.Threading.Tasks;

namespace src.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IUserValidator<ApplicationUser> userValidator;
        private IPasswordValidator<ApplicationUser> passwordValidator;
        private IPasswordHasher<ApplicationUser> passwordHasher;

        public AdminController(UserManager<ApplicationUser> usrMgr, IUserValidator<ApplicationUser> userValid, IPasswordValidator<ApplicationUser> passwordValid, IPasswordHasher<ApplicationUser> passHash)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passwordValid;
            passwordHasher = passHash;
        }

        public ViewResult Index() => View(userManager.Users);

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found!");
            }
            return View("Index", userManager.Users);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> Edit (string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);
                if(!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if(!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                    if(validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if((validEmail.Succeeded && validPass ==null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result= await userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("","User Not Found");
            }
            return View(user);
        }
    }
}