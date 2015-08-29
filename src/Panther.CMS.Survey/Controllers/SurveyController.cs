using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc;

namespace Panther.CMS.Survey.Controllers
{
    public class SurveyController : Controller
    {
        [HttpPost]
        public IActionResult SaveSurvey(FormCollection collection, string surveyName)
        {


            return View();
        }
    }
}
