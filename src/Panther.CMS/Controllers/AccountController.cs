using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

using Panther.CMS.Entities;
using Panther.CMS.Interfaces;
using Panther.CMS.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Panther.CMS.Controllers
{
    public class AccountController : PantherController
    {
        public AccountController(IPantherContext context, UserManager<User> userManager, SignInManager<User> signInManager ) : base(context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<User> UserManager { get; private set; }
        public SignInManager<User> SignInManager { get; private set; }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginProvider = SignInManager.GetExternalAuthenticationSchemes().ToList();
            return CurrentPage();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return PartialView(model);

            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if(result.Succeeded)
                return RedirectToLocal(returnUrl);
            //if (result.RequiresTwoFactor)
            //    RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            if (result.IsLockedOut)
                return PartialView("LockedOut");

            ModelState.AddModelError("", "Invalid login attempt.");

            return PartialView(model);
        }

        public async Task<IActionResult> Logoff(string returnUrl)
        {
            await SignInManager.SignOutAsync();
            return RedirectToLocal(returnUrl);
        }

        public IActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl)
                ? Redirect(returnUrl)
                : Redirect("/");
        }
#if DEBUG
        public async Task<IActionResult> GetToken()
        {
            var user = await UserManager.FindByEmailAsync("pmdevers@gmail.com");
            var token = await UserManager.GenerateUserTokenAsync(user, "Email", "test");

            return Content(token);
        }

        public async Task<IActionResult> SetToken(string token)
        {
            var user = await UserManager.FindByEmailAsync("pmdevers@gmail.com");
            var valid = await UserManager.VerifyUserTokenAsync(user, "Email", "test", token);
            return Content(valid.ToString());
        }
#endif
    }
}
