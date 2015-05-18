using Microsoft.Framework.DependencyInjection;

namespace Panther.CMS.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPanther(this IServiceCollection services)
        {
            services.AddMvc();
            PantherServices.GetDefaultServices(services);
            return services;
        }
    }
}