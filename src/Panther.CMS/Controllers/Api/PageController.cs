using System;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc;

using Panther.CMS.Entities;
using Panther.CMS.Services.Page;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Panther.CMS.Controllers.Api
{
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private IPageService pageService;

        public PageController(IPageService pageService)
        {
            this.pageService = pageService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Page> Get()
        {
            return pageService.Get();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Page Get(Guid id)
        {
            return pageService.Get(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Page page)
        {
            pageService.Post(page);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}