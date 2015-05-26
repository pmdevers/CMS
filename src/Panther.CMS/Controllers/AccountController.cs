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
        public IActionResult Index(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginProvider = SignInManager.GetExternalAuthenticationSchemes().ToList();
            return CurrentPage();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, false, shouldLockout: false);
                return RedirectToLocal(returnUrl);
            }

            return CurrentPage(model);
        }

        public IActionResult Logoff(string returnUrl)
        {
            SignInManager.SignOut();
            return RedirectToLocal(returnUrl);
        }

        public IActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl)
                ? Redirect(returnUrl)
                : Redirect("/");
        }
    }
}
