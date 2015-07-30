using Microsoft.Framework.DependencyInjection;

using Panther.CMS;
using Panther.CMS.Entities;
using Panther.CMS.Filters;
using Panther.CMS.Setup;
using Panther.CMS.Storage.Role;
using Panther.CMS.Storage.User;

namespace Microsoft.Framework.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPanther(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();
            services.AddMvc();
            services.ConfigureMvc(options =>
            {
                options.Filters.Add(new SecurityFilterAttribute());
            });
            services.ConfigureRazorViewEngine(razor =>
            {
                razor.ViewLocationExpanders.Add(new PantherViewLocationExpander());
            });
            PantherServices.GetDefaultServices(services);
            return services;
        }
    }
}