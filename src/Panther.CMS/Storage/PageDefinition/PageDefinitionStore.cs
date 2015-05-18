using System;

using Panther.CMS.Interfaces;

namespace Panther.CMS.Storage.PageDefinition
{
    public class PageDefinitionStore : Store<Entities.PageDefinition, Guid>
    {
        public PageDefinitionStore(IPantherFileSystem fileSystem) : base(fileSystem)
        { }

        public override Guid GenerateKey()
        {
            return Guid.NewGuid();
        }
    }
}