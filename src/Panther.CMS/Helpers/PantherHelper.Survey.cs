using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc.Rendering;

using Panther.CMS.Helpers;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static SurveyHelper Survey(this IHtmlHelper helper, string surveyName)
        {
            return new SurveyHelper(helper, surveyName);
        }
    }

    public enum SurveyAction
    {
        Next,
        Previous,
        Reset,
        Mail
    }
}
