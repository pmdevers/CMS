using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.Media
{
    public class MediaStore : Store<Entities.Media, Guid>, IMediaStore
    {
        public MediaStore(IPantherFileSystem fileSystem) : base(fileSystem)
        {
        }

        public override Guid GenerateKey()
        {
            return Guid.NewGuid();
        }
    }
}
