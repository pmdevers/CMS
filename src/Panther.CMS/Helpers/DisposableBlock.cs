using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;

namespace Panther.CMS.Helpers
{
    public class DelayedBlock : IDisposable
    {
        private readonly string blockName;
        private readonly IHtmlHelper htmlHelper;
        private readonly TextWriter textWriter;
        private readonly StringBuilder stringBuilder;


        public static string GetBlock(IHtmlHelper htmlHelper, string blockName)
        {
            var blocks = htmlHelper.ViewContext.HttpContext.Items[blockName] as List<string>;
            if(blocks == null)
                return string.Empty;
            var sb = new StringBuilder();
            foreach (var block in blocks)
            {
                sb.AppendLine(block);
            }
            return sb.ToString();
        }

        private void AddBlock(string content)
        {
            if (htmlHelper.ViewContext.HttpContext.Items[blockName] == null)
                htmlHelper.ViewContext.HttpContext.Items[blockName] = new List<string>();
            var block = htmlHelper.ViewContext.HttpContext.Items[blockName] as List<string>;
            block.Add(content);
        }

        public DelayedBlock(IHtmlHelper htmlHelper, string blockName)
        {
            this.blockName = blockName;
            this.htmlHelper = htmlHelper;
            this.textWriter = htmlHelper.ViewContext.Writer;
            this.stringBuilder = new StringBuilder();
            htmlHelper.ViewContext.Writer = new StringWriter(stringBuilder);
        }

        public void Dispose()
        {
            AddBlock(StripTags(stringBuilder.ToString()));
            htmlHelper.ViewContext.Writer = textWriter;
        }

        public string StripTags(string htmlSource)
        {
            var pattern = @"<"+ blockName + "[^>]*?>(.*?)</" + blockName + ">";
            var matches = Regex.Matches(htmlSource, pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var l = new ArrayList();
            foreach (Match match in matches)
            {
                l.Add(match.Groups[1].Value);
            }

            return string.Join(" ", l.ToArray());
        }

    }
}
