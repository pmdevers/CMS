using System.Collections.Generic;
using System.Text;

using Microsoft.AspNet.Mvc.Rendering;

namespace Microsoft.AspNet.Mvc.Rendering
{
    public static partial class PantherHelper
    {
        public static HtmlString Scripts(this IHtmlHelper htmlHelper)
        {
            var scripts = htmlHelper.Panther().Site.Scripts;
            scripts.ForEach(htmlHelper.RegisterScript);
            return htmlHelper.RenderRegisteredScripts();
        }

        public static HtmlString RenderRegisteredScripts(this IHtmlHelper htmlHelper)
        {
            var ctx = htmlHelper.ViewContext.HttpContext;
            var registeredScripts = ctx.Items["_scripts_"] as Stack<string>;
            if (registeredScripts == null || registeredScripts.Count < 1)
            {
                return null;
            }
            var sb = new StringBuilder();
            foreach (var script in registeredScripts)
            {
                var scriptBuilder = new TagBuilder("script");
                scriptBuilder.Attributes["type"] = "text/javascript";
                scriptBuilder.Attributes["src"] = script;
                sb.AppendLine(scriptBuilder.ToString(TagRenderMode.Normal));
            }
            return new HtmlString(sb.ToString());
        }

        public static void RegisterScript(this IHtmlHelper htmlHelper, string script)
        {
            var ctx = htmlHelper.ViewContext.HttpContext;

            var registeredScripts = ctx.Items["_scripts_"] as Stack<string>;
            if (registeredScripts == null)
            {
                registeredScripts = new Stack<string>();
                ctx.Items["_scripts_"] = registeredScripts;
            }
            var src = script;
            if (!registeredScripts.Contains(src))
            {
                registeredScripts.Push(src);
            }
        }
    }
}