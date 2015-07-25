using System.IO;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

using Panther.CMS.Filters;
using Panther.CMS.Interfaces;
using Panther.CMS.Models;
using Panther.Mail.Mvc;

namespace Panther.CMS.Controllers
{
    [SecurityFilter]
    public class PantherController : Controller
    {
        private readonly IPantherContext context;
        public PantherController(IPantherContext context)
        {
            this.context = context;
        }

        protected IPantherContext PantherContext
        {
            get { return context; }
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
#if DEBUG
        public IActionResult Test()
        {
            var mail = new TestMail();

            throw new System.Exception("Error");
            

            mail.Test = "test test";

            return new EmailResult(mail);
        }

        public class TestMail : Email
        {
            public TestMail (): base("Test") { }

            public string Test { get; set; }
        }
#endif
    }
}