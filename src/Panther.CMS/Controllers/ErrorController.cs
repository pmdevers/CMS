using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc;
using Panther.CMS.Extensions;
using Panther.CMS.Interfaces;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Diagnostics.Views;
using Panther.CMS.Models;

namespace Panther.CMS.Controllers
{
    public sealed class ErrorController : Controller
    {
        public ErrorController(IPantherContext context)
        {

        }

        #region Public Methods

        // [OutputCache(CacheProfile = CacheProfileName.Error)]
        public ActionResult Error(int statusCode, string status)
        {
            var error = Context.GetFeature<IErrorHandlerFeature>();

            this.Response.StatusCode = statusCode;
            var model = new ErrorPageModel();
            if (error != null)
            {
                model = new ErrorPageModel
                {
                    ErrorDetails = GetErrorDetails(error.Error, false),
                    Headers = Request.Headers,
                    Query = Request.Query,
                };
            }
            ActionResult result;
            if (this.Request.IsAjaxRequest())
            {
                // This allows us to show errors even in partial views.
                result = this.PartialView(statusCode.ToString(), model);
            }
            else
            {
                result = this.View(statusCode.ToString(), model);
            }

            return result;
        }


        private IEnumerable<ErrorDetails> GetErrorDetails(Exception ex, bool showSource)
        {
            for (Exception scan = ex; scan != null; scan = scan.InnerException)
            {
                yield return new ErrorDetails
                {
                    Error = scan,
                    //StackFrames = StackFrames(scan, showSource)
                };
            }
        }
        #endregion
    }
}
