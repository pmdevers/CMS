using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Entities
{
    public class User : IEntity<Guid>
    {
        public User()
        {
            Roles = new List<string>();
            Claims = new List<UserClaim>();
        }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Guid Id { get; set; }

        public List<string> Roles { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled{ get; set; }
        public string SecurityToken { get; set; }
        public List<UserClaim> Claims { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }

    public class UserClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
