using Panther.CMS.Interfaces;

namespace Panther.CMS.Plugin
{
    public class PluginManager
    {
        public IPantherFileSystem _fileSystem;

        public PluginManager(IPantherFileSystem filesystem)
        {
            _fileSystem = filesystem;
        }

        public void AddDependency(string dependency, string version)
        {
        }
    }
}