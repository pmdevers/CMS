using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;
using System.Globalization;

namespace Panther.CMS.Storage.Resource
{
    public class ResourceStore : Store<Entities.Resource, string>, IResourceStore
    {
        public ResourceStore(IPantherFileSystem fileSystem) : base(fileSystem)
        {
        }

        public override string CreateFilename()
        {

#if DNXCORE50
            
            var culture = CultureInfo.CurrentUICulture;
#else
            var culture = Thread.CurrentThread.CurrentUICulture;
#endif
            return "~/App_Data/resources." + culture.Name + ".json";
        }

        public string GetResource(string key)
        {
            var resource = this.GetByKey(key);
            if (resource == null)
            {
                resource = new Entities.Resource { Id = key, Value = "MR - Key" };
                Add(resource);
            }
            return resource.Value;

        }
    }
}
