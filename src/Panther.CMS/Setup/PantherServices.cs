using Microsoft.Framework.DependencyInjection;

using Panther.CMS.Interfaces;
using Panther.CMS.Routing;
using Panther.CMS.Services.Content;
using Panther.CMS.Services.Page;
using Panther.CMS.Services.Site;
using Panther.Mail.Mvc;

using PantherCMS;

namespace Panther.CMS.Setup
{
    public class PantherServices
    {
        public static void GetDefaultServices(IServiceCollection collection)
        {
            //var describe = new ServiceDescriptor(configuration);
            //Options
            collection.AddTransient<IPageService, PageService>();
            collection.AddTransient<ISiteService, SiteService>();
            collection.AddTransient<IContentService, ContentService>();
            collection.AddSingleton<IPantherContext, PantherContext>();
            collection.AddTransient<IPantherFileSystem, PantherFileSystem>();
            collection.AddTransient<IPantherRouter, PantherRouter>();
            collection.AddTransient<IEmailParser, EmailParser>();
            //Storages

            //yield return describe.Describe(typeof(IService<>), typeof(Service<>), implementationInstance: null, lifecycle: LifecycleKind.Transient);
        }
    }
}