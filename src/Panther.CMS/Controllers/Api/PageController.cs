using System;
using System.Collections.Generic;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

using Panther.CMS.Controllers.Api.Models;
using Panther.CMS.Entities;
using Panther.CMS.Interfaces;
using Panther.CMS.Services.Page;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Panther.CMS.Controllers.Api
{
    [Route("api/site/pages")]
    public class PageController
    {
        private readonly IPantherContext context;

        public PageController(IPantherContext context)
        {
            this.context = context;
        }

        // GET: api/value       
        [HttpGet]
        public PageRepresentation Get()
        {
            var root = context.Root;

            var model = new PageRepresentation
            {
                Name = root.Name,
                Id = root.Id
            };
            model.Links.Add(LinkTemplates.GetSite());
            model.Links.Add(LinkTemplates.GetPages());

            return model;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IList<Page> Get(Guid id)
        {
            return context.Root.GetById(id).Children;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Page page)
        {
            //pageService.Post(page);
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