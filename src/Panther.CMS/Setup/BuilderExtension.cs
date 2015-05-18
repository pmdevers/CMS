using System;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;

using Panther.CMS.Constraints;

using PantherCMS;

namespace Panther.CMS.Setup
{
    /// <summary>
    /// Summary description for BuilderExtensions
    /// </summary>
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UsePanther(this IApplicationBuilder app)
        {
            UsePanther(app, config =>
            {
                config.Test = "Test";

                config.Routes = (routes =>
                {

                    //routes.MapRoute(null, "api/{controller}/{action}", new { controller = "Templates", action = "Handle" });
                    routes.MapRoute(null, "api/{controller}", new { controller = "Templates" });
                    routes.MapRoute("default", "{controller}/{action}", new { controller = "Home", action = "Index" }, new { route = new TestConstaint() });
                    routes.MapRoute("defaultCulture", "{culture}/{*url}", new { culture = "nl-nl", controller = "Panther", action = "CurrentPage" }, new { host = new HostConstraint() });
                    routes.MapRoute("cmsroute", "{*url}", new { culture = "nl-nl", controller = "Panther", action = "CurrentPage" }, new { host = new HostConstraint() });
                });
            });

            return app;
        }

        public static IApplicationBuilder UsePanther(this IApplicationBuilder app, Action<IPantherConfig> config)
        {
            var pantherConfig = new PantherConfig();
            var staticFileOptions = new StaticFileOptions();

            config(pantherConfig);

            app.UseStaticFiles(staticFileOptions)
            .UseIdentity()
            .UseCookieAuthentication(options =>
            {
                options.LoginPath = new PathString("/login");
            })
            .UseMiddleware<PantherMiddleware>()
            .UseMvc(pantherConfig.Routes);

            return app;
        }
    }
}