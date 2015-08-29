using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc.Rendering;

namespace Panther.CMS.Survey.Helpers
{
    public class SurveyHelper
    {
        readonly IHtmlHelper htmlHelper;
        readonly string surveyName;

        public SurveyHelper(IHtmlHelper htmlHelper, string surveyName)
        {
            this.htmlHelper = htmlHelper;
            this.surveyName = surveyName;
        }

        public string GetValue(string field)
        {
            var cachedFields = GetFromCache();
            if (cachedFields.ContainsKey(field))
                return cachedFields[field];
            return string.Empty;
        }


        private Dictionary<string, string> GetFromCache()
        {
            Cookie context = htmlHelper.Panther().Cookie;
            
        }
    }
}
