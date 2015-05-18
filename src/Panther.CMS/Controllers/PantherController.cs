using Microsoft.AspNet.Mvc;

using Panther.CMS.Interfaces;
using Panther.CMS.Models;
using Panther.Mail.Mvc;

namespace Panther.CMS.Controllers
{
    public class PantherController : Controller
    {
        private IPantherContext context;
        public PantherController(IPantherContext context)
        {
            this.context = context;
        }

        public IActionResult RedirectToCurrentPage()
        {
            return Redirect(context.Path);
        }

        public IActionResult CurrentPage()
        {
            return View(context.Current.Template);  
        }

        protected IActionResult CurrentPage(object model)
        {
            return View(context.Current.Template, model);
        }

        [HttpPost]
        public IActionResult CurrentPage(HandleModel model)
        {
            return CurrentPage();
        }

        public IActionResult Test()
        {
            var mail = new TestMail();

            mail.Test = "test test";

            return new EmailResult(mail);
        }

        public class TestMail : Email
        {
            public TestMail (): base("Test") { }

            public string Test { get; set; }
        }
    }
}