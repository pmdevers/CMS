using System;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;

using Panther.CMS;
using Panther.CMS.Constraints;

namespace Microsoft.Framework.DependencyInjection
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
                config.LoginPath = "/login";
            });

            return app;
        }

        public static IApplicationBuilder UsePanther(this IApplicationBuilder app, Action<PantherConfig> config)
        {

            var pantherConfig = new PantherConfig();
            config(pantherConfig);
            var staticFileOptions = new StaticFileOptions();

            app.UseStaticFiles(staticFileOptions)
            .UseIdentity()
            .UseCookieAuthentication(options =>
            {
                options.LoginPath = new PathString(pantherConfig.LoginPath);
                options.AutomaticAuthentication = true;
            })
            .UseMiddleware<PantherMiddleware>()
            .UseMvc(routes =>
            {
                //routes.MapRoute(null, "api/{controller}/{action}", new { controller = "Templates", action = "Handle" });
                routes.MapRoute(null, "api/{controller}", new { controller = "Templates" });
                routes.MapRoute("default", "{controller}/{action}", new { controller = "Home", action = "Index" }, new { route = new TestConstaint() });
                routes.MapRoute("defaultCulture", "{culture}/{*url}", new { culture = "nl-nl", controller = "Panther", action = "CurrentPage" }, new { host = new HostConstraint() });
                routes.MapRoute("cmsroute", "{*url}", new { culture = "nl-nl", controller = "Panther", action = "CurrentPage" }, new { host = new HostConstraint() });
            });
            return app;
        }
    }
}