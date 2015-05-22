using Microsoft.AspNet.Authorization;
using Microsoft.Framework.DependencyInjection;

using Panther.CMS.Entities;
using Panther.CMS.Filters;
using Panther.CMS.Setup;

namespace Microsoft.Framework.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPanther(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>();
            services.AddMvc();
            services.ConfigureMvc(options =>
            {
                options.Filters.Add(new SecurityFilterAttribute());
            });
            PantherServices.GetDefaultServices(services);
            return services;
        }
    }
}