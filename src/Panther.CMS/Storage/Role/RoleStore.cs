using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Panther.CMS.Entities;
using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Role
{
    public class RoleStore : Store<Entities.Role, Guid>,
        IRoleClaimStore<Entities.Role>
    {
        public RoleStore(IPantherFileSystem fileSystem) : base(fileSystem)
        {
        }

        public override Guid GenerateKey()
        {
            return Guid.NewGuid();
        }

        public Task<IdentityResult> CreateAsync(Entities.Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            Add(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(Entities.Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            Update(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(Entities.Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            Delete(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<string> GetRoleIdAsync(Entities.Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(ConvertIdToString(role.Id));
        }

        public Task<string> GetRoleNameAsync(Entities.Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(Entities.Role role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            role.Name = roleName;
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedRoleNameAsync(Entities.Role role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            return Task.FromResult(role.Name.ToUpper());
        }

        public Task SetNormalizedRoleNameAsync(Entities.Role role, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task<Entities.Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var id = ConvertIdFromString(roleId);
            var role = GetByKey(id);
            return Task.FromResult(role);
        }

        public Task<Entities.Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var role = FindAll(x => x.Name.ToUpper() == normalizedRoleName).FirstOrDefault();
            return Task.FromResult(role);
        }

        public Task<IList<Claim>> GetClaimsAsync(Entities.Role role, CancellationToken cancellationToken = new CancellationToken())
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var claims = role.Claims.Select(claim => new Claim(claim.Type, claim.Value)).ToList();

            return Task.FromResult((IList<Claim>)claims);
        }

        public Task AddClaimAsync(Entities.Role role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            role.Claims.Add(new RoleClaim() { Type = claim.Type, Value = claim.Value});
            return Task.FromResult(false);
        }

        public Task RemoveClaimAsync(Entities.Role role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
        {
            ThrowIfDisposed();
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            var claims = role.Claims.Where(x => x.Value == claim.Value && x.Type == claim.Type).ToList();
            foreach (var c in claims)
            {
                role.Claims.Remove(c);
            }

            return Task.FromResult(0);
        }
    }
}
