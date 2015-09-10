using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Framework.Caching.Memory;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Helpers
{
    public class SurveyHelper
    {
        public const string ButtonNext = "surveyButton";
        readonly IHtmlHelper htmlHelper;
        readonly IPantherContext context;
        readonly string surveyName;

        public SurveyHelper(IPantherContext context, string surveyName)
        {
            this.context = context;
            this.surveyName = surveyName;
        }

        public SurveyHelper(IHtmlHelper htmlHelper, string surveyName)
        {
            this.htmlHelper = htmlHelper;
            this.context = htmlHelper.Panther();
            this.surveyName = surveyName;
        }

        public IHtmlContent TextBox(string fieldName)
        {
            return htmlHelper.TextBox(fieldName, GetValue(fieldName));
        }

        public IHtmlContent MultiLine(string fieldName, int rows, int columns)
        {
            return htmlHelper.TextArea(fieldName, GetValue(fieldName).ToString(), rows, columns, null);
        }

        public IHtmlContent Radio(string fieldName, string value)
        {
            var currentValue = GetValue(fieldName);
            var isChecked = currentValue.ToString() == value;
            return htmlHelper.RadioButton(fieldName, value, isChecked);
        }

        public IHtmlContent CheckBox(string fieldName, bool defaultValue = false)
        {
            var value = GetValue(fieldName).ToString();
            var ticked = !string.IsNullOrEmpty(value) ? value != "false" : defaultValue;
            return htmlHelper.CheckBox(fieldName, ticked);
        }

        public IHtmlContent DropDown(string fieldName, IEnumerable items, string dataValueField, string dataTextField, string label)
        {
            var value = GetValue(fieldName).ToString();
            var list = new SelectList(items, dataValueField, dataTextField, value);
            return htmlHelper.DropDownList(fieldName, list, label);
        }

        public IHtmlContent Button(string label, SurveyAction action)
        {
            if(action == SurveyAction.Reset)
                Clear();
            return new HtmlString($"<button name=\"{ButtonNext}\" type=\"submit\" value=\"{action}\">{label}</button>");
        }

        public IHtmlContent SurveyStart()
        {
            return htmlHelper.Partial($"~/views/survey/{surveyName}/start");
        }

        public IDisposable BeginSurvey()
        {
            return
                htmlHelper.AjaxForm(new AjaxOptions
                {
                    FormMethod = FormMethod.Post,
                    InsertionMode = InsertionMode.Replace,
                    JQuerySelector = "#survey",
                    Url = $"/survey/post?surveyName={surveyName}"
                });
        }

        public void Clear()
        {
            var cacheKey = GetUniqueKey();
            context.SetCached<Dictionary<string, object>>(cacheKey, null);
        }

        public IHtmlContent GetValue(string field)
        {
            var cacheKey = GetUniqueKey();
            var key = field.ToLower();
            var cachedFields = context.GetCached<Dictionary<string, object>>(cacheKey);
            var content = cachedFields.ContainsKey(key) ? cachedFields[key].ToString() : string.Empty;
            return new HtmlString(content);
        }

        public void AddValues(string field, object value)
        {
            var cacheKey = GetUniqueKey();
            var key = field.ToLower();
            var cachedFields = context.GetCached<Dictionary<string, object>>(cacheKey);
            cachedFields[key] = value;
            context.SetCached(surveyName, cachedFields);
        }

        private string GetUniqueKey()
        {
            if(!context.Cookie.Exists(surveyName))
                context.Cookie.Set(surveyName, Guid.NewGuid().ToString("N"));
            return context.Cookie.Get(surveyName);
        }

        public void Next(string viewName)
        {
            AddValues(SurveyAction.Next.ToString(), viewName);
        }
        public void Previous(string viewName)
        {
            AddValues(SurveyAction.Previous.ToString(), viewName);
        }
    }
}
