using System;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

using Panther.CMS.Entities;

namespace Panther.CMS.Setup
{
    public static class Initializer
    {
        private const string defaultAdminRole = "DefaultAdminRole";
        private const string defaultAdminUserName = "DefaultAdminUserName";
        private const string defaultAdminPassword = "DefaultAdminPassword";

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var configuration = new Configuration()
                        .AddJsonFile("config.json")
                        .AddEnvironmentVariables();

            var userManager = serviceProvider.GetService<UserManager<User>>();
            var roleManager = serviceProvider.GetService<RoleManager<Role>>();
            var defaultAdminRoleValue = configuration.Get<string>(defaultAdminRole);
            if (!await roleManager.RoleExistsAsync(defaultAdminRoleValue))
            {
                await roleManager.CreateAsync(new Role {Name = defaultAdminRoleValue });
            }

            var user = await userManager.FindByNameAsync(configuration.Get<string>(defaultAdminUserName));
            if (user == null)
            {
                user = new User { Username = configuration.Get<string>(defaultAdminUserName) };
                await userManager.CreateAsync(user, configuration.Get<string>(defaultAdminPassword));
                await userManager.AddToRoleAsync(user, defaultAdminRoleValue);
                await userManager.AddClaimAsync(user, new Claim("Administration", "Allowed"));
            }
        }
    }
}