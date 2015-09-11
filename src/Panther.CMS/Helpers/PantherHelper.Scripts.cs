using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.AspNet.Html.Abstractions;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Framework.WebEncoders;

using Panther.CMS.Helpers;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        private const string ScriptKey = "_scripts_";
        private const string ScriptBlockKey = "_scriptblocks_";

        public static IHtmlContent Scripts(this IHtmlHelper htmlHelper)

        {
            var scripts = htmlHelper.Panther().Site.Scripts;
            scripts.ForEach(htmlHelper.RegisterScript);

            var sb = new StringBuilder();
            sb.Append(htmlHelper.RenderRegisteredScripts());
            
            var script = DelayedBlock.GetBlock(htmlHelper, "script");
            var minScript = Minifier.MinifyJavascript(script);

            if (!string.IsNullOrWhiteSpace(minScript))
            {
                sb.Append("<script type=\"text/javascript\">");
                sb.Append(minScript);
                sb.Append("</script>");
            }
            return new HtmlString(sb.ToString().Replace("/n/r", ""));
        }

        private static string RenderRegisteredScripts(this IHtmlHelper htmlHelper)
        {
            var ctx = htmlHelper.ViewContext.HttpContext;
            var registeredScripts = ctx.Items[ScriptKey] as Stack<string>;
            if (registeredScripts == null || registeredScripts.Count < 1)
            {
                return null;
            }
            var sb = new StringBuilder();
            foreach (var script in registeredScripts)
            {
                sb.AppendLine($"<script type=\"text/javascript\" src=\"{script}\" />");
            }
            return sb.ToString();
        }

        //private static string RenderRegisterdScriptBlocks(this IHtmlHelper htmlHelper)
        //{
        //    var ctx = htmlHelper.ViewContext.HttpContext;
        //    var registeredScripts = ctx.Items[ScriptBlockKey] as Dictionary<string, Action<string>>;
        //    if (registeredScripts == null || registeredScripts.Count < 1)
        //    {
        //        return null;
        //    }
        //    var sb = new StringBuilder();
        //    foreach (var script in registeredScripts)
        //    {

        //        sb.Append(script.Value);
        //    }

        //    var scriptBuilder = new TagBuilder("script");
        //    scriptBuilder.Attributes["type"] = "text/javascript";
        //    scriptBuilder.SetInnerText(sb.ToString());
        //    return scriptBuilder.ToString(TagRenderMode.EndTag);
        //}

        public static IDisposable RegisterScript(this IHtmlHelper htmlHelper)
        {
            return new DelayedBlock(htmlHelper, "script");
        }
        public static void RegisterScript(this IHtmlHelper htmlHelper, string script)
        {
            var ctx = htmlHelper.ViewContext.HttpContext;
            var registeredScripts = ctx.Items[ScriptKey] as Stack<string>;
            if (registeredScripts == null)
            {
                registeredScripts = new Stack<string>();
                ctx.Items[ScriptKey] = registeredScripts;
            }
            var src = script;
            if (!registeredScripts.Contains(src))
            {
                registeredScripts.Push(src);
            }
        }
    }
}