using System;
using System.Collections.Generic;
using System.Linq;

using Panther.CMS.Components.Content;
using Panther.CMS.Interfaces;
using Panther.CMS.Services.Models;
using Panther.CMS.Services.Page;

namespace Panther.CMS.Services.Content
{
    public class ContentService : BaseService, IContentService
    {
        public ContentService(IPantherContext context) : base(context)
        {
        }

        public List<ContentModel> GetContent(string url)
        {
            var contentComponent = new ContentComponent(Context);
            var pageService = new PageService(Context);
            var page = pageService.GetPage(Context.Root, url);
            var content = contentComponent.GetPageContent(page);

            var contentModels = content.Select(x => ContentModel(x)).ToList();

            return contentModels;
        }

        private ContentModel ContentModel(Entities.Content content, ContentModel parent = null)
        {
            var itemModel = new ContentModel
            {
                Name = content.Name,
                Data = content.Data,
                Type = content.Type
            };

            foreach(var item in content.Children)
            {
                var child = ContentModel(item, itemModel);
                itemModel.Children.Add(child);
            }

            return itemModel;
        }


        public void AddToPage(string url, List<ContentModel> model)
        {
            var contentComponent = new ContentComponent(Context);
            var pageComponent = new PageService(Context);

            var page = pageComponent.GetPage(Context.Root, url);

            if (page == null)
            {
                throw new Exception("Page does not exist!");
            }

            contentComponent.DeleteContent(page);

            foreach (var content in model)
            {
                var contentTree = GetContentTreeFromModel(content);

                contentComponent.AddContent(page, contentTree);
            }
        }

        private Entities.Content GetContentTreeFromModel(ContentModel model)
        {
            var content = new Entities.Content
            {
                Name = model.Name,
                Type = model.Type,
                Data = model.Data
            };

            foreach (var child in model.Children)
            {
                var contentChild = GetContentTreeFromModel(child);
                content.Children.Add(contentChild);
            }

            return content;
        }

        public void AddToSite(ContentModel model)
        {
            throw new NotImplementedException();
        }
    }
}