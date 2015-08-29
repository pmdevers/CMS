using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;

using Panther.CMS.Helpers;
using Panther.CMS.Interfaces;

namespace Panther.CMS.Controllers
{
    public class SurveyController : Controller
    {
        readonly IPantherContext context;
        public SurveyController(IPantherContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public IActionResult Post(string surveyName, FormCollection collection, SurveyAction surveyButton)
        {
            var survey = new SurveyHelper(context, surveyName);

            foreach (var key in collection.Keys)
            {
                survey.AddValues(key, collection.Get(key));
            }
            switch (surveyButton)
            {
                case SurveyAction.Previous:
                case SurveyAction.Next:
                    return View(surveyName + "/" + survey.GetValue(surveyButton.ToString()));
                case SurveyAction.Reset:
                    survey.Clear();
                    return View(surveyName + "/start");
                case SurveyAction.Mail:
                    return View(surveyName + "/end");
                default:
                    throw new ArgumentOutOfRangeException(nameof(surveyButton), surveyButton, null);
            }
        }
    }
}
