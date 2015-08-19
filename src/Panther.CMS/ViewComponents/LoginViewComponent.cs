using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Panther.CMS.Entities;

namespace Panther.CMS.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public LoginViewComponent(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<User> UserManager { get; private set; }
        public SignInManager<User> SignInManager { get; private set; }

        public IViewComponentResult Invoke()
        {
            return View("~/templates/login");
        }
    }
}
