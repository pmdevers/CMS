using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Resource
{
    public interface IResourceStore : IStore<Entities.Resource, string>
    {
        string GetResource(string key);
    }
}
