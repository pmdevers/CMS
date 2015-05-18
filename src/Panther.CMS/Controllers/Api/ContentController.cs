using System.Collections.Generic;

using Microsoft.AspNet.Mvc;

using Panther.CMS.Services.Content;
using Panther.CMS.Services.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Panther.CMS.Controllers.Api
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
        private IContentService contentSerivce;

        public ContentController(IContentService contentSerivce)
        {
            this.contentSerivce = contentSerivce;
        }

        // GET: api/values
        [HttpGet("{*url}")]
        public IEnumerable<ContentModel> Get(string url = "")
        {
             return contentSerivce.GetContent(url);
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody]ContentModel value)
        //{
        //    contentSerivce.AddToSite(value);
        //}

        [HttpPost("{*url}")]
        public void Post([FromBody]List<ContentModel> model, string url = "")
        {
            contentSerivce.AddToPage(url, model);
        }

        // PUT api/values/5
        [HttpPut("{url}")]
        public void Put(string url, [FromBody]List<ContentModel> value)
        {
            contentSerivce.AddToPage(url, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}