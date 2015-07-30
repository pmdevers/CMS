using System.IO;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

using Panther.CMS.Filters;
using Panther.CMS.Interfaces;
using Panther.CMS.Models;

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
            var view = View(context.Current.Template);
            view.ViewData["Layout"] = "Layout";
            return view;
        }

        protected IActionResult CurrentPage(object model)
        {
            var view = View(context.Current.Template, model);
            view.ViewData["Layout"] = "Layout";
            return view;
        }

        [HttpPost]
        public IActionResult CurrentPage(HandleModel model)
        {
            return CurrentPage();
        }
    }
}