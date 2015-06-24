using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
//using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;


using Panther.CMS.Setup;

namespace Panther.CMS.Demo
{
    public class Startup
    {
        //public IConfiguration Configuration { get; private set; }
        public Startup()
        {
            //Configuration = new Configuration();
            //                .AddJsonFile("config.json")
        }
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPanther();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UsePanther(config =>
            {
                config.LoginPath = "/login";
            });

            //app.UsePanther();
        }
    }
}
