using System;
using System.Globalization;
using System.IO;

using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;

using Newtonsoft.Json;

using Panther.CMS.Interfaces;

namespace Panther.CMS
{
    public class PantherFileSystem : IPantherFileSystem
    {
        private readonly IServiceProvider services;
        private IHostingEnvironment hostingEnvironment;
        private const int DefaultBufferSize = 0x1000;

        public PantherFileSystem(IServiceProvider services)
        {
            this.services = services;

            FileProvider = GetFileSystem(services);
        }

        public IFileProvider FileProvider { get; set; }

        public string Extension
        {
            get
            {
                return ".json";
            }
        }

        public void CreateFile(string filename)
        {
            lock (locker)
            {
                File.Create(filename);
            }
        }

        public string CreateFilename(Type type)
        {
            return "~/App_Data/" + type.Name + ".json";
        }

        public bool FileExists(string filename)
        {
            try
            {
                filename = ResolveFilePath(FileProvider, filename);
            }
            catch (FileNotFoundException)
            {
                return false;
            }

            return !string.IsNullOrEmpty(filename);
        }

        public T ReadFile<T>(string filename)
        {
            var fileContent = ReadFile(filename);

            return JsonConvert.DeserializeObject<T>(fileContent);
        }

        public void WriteToFile<T>(string filename, T content)
        {
            var json = JsonConvert.SerializeObject(content, Formatting.Indented);
            WriteToFile(filename, json);
        }

        private IFileProvider GetFileSystem(IServiceProvider requestServices)
        {
            if (FileProvider != null)
            {
                return FileProvider;
            }

            // For right now until we can use IWebRootFileSystemProvider, see
            // https://github.com/aspnet/Hosting/issues/86 for details.
            hostingEnvironment = requestServices.GetService<IHostingEnvironment>();
            FileProvider = new PhysicalFileProvider(hostingEnvironment.WebRootPath);

            return FileProvider;
        }

        internal string ResolveFilePath(IFileProvider fileProvider, string filename)
        {
            // Let the file system try to get the file and if it can't,
            // fallback to trying the path directly unless the path starts with '/'.
            // In that case we consider it relative and won't try to resolve it as
            // a full path

            var path = NormalizePath(filename);

            if (IsPathRooted(path))
            {
                // The path is absolute
                // C:\...\file.ext
                // C:/.../file.ext
                return path;
            }

            IFileInfo fileInfo = fileProvider.GetFileInfo(path);

            

            if (!fileInfo.Exists)
            {

                CreateFile(hostingEnvironment.WebRootPath + path);
                fileInfo = fileProvider.GetFileInfo(path);
                //// We are absolutely sure the path is relative, and couldn't find the file
                //// on the file system.
                //var message = "File Not Found"; //Resources.FormatFileResult_InvalidPath(path);
                //throw new FileNotFoundException(message, path);
            }

            // The path is relative and IFileSystem found the file, so return the full
            // path.
            return fileInfo.PhysicalPath;
        }

        // Internal for unit testing purposes only
        /// <summary>
        /// Creates a normalized representation of the given <paramref name="path"/>. The default
        /// implementation doesn't support files with '\' in the file name and treats the '\' as
        /// a directory separator. The default implementation will convert all the '\' into '/'
        /// and will remove leading '~' characters.
        /// </summary>
        /// <param name="path">The path to normalize.</param>
        /// <returns>The normalized path.</returns>
        protected internal virtual string NormalizePath(string path)
        {
            // Unix systems support '\' as part of the file name. So '\' is not
            // a valid directory separator in those systems. Here we make the conscious
            // choice of replacing '\' for '/' which means that file names with '\' will
            // not be supported.

            if (path.StartsWith("~/", StringComparison.Ordinal))
            {
                // We don't support virtual paths for now, so we just treat them as relative
                // paths.
                return path.Substring(1).Replace('\\', '/');
            }

            if (path.StartsWith("~\\", StringComparison.Ordinal))
            {
                // ~\ is not a valid virtual path, and we don't want to replace '\' with '/' as it
                // ofuscates the error, so just return the original path and throw at a later point
                // when we can't find the file.
                return path;
            }

            return path.Replace('\\', '/');
        }

        // Internal for unit testing purposes only
        /// <summary>
        /// Determines if the provided path is absolute or relative. The default implementation considers
        /// paths starting with '/' to be relative.
        /// </summary>
        /// <param name="path">The path to examine.</param>
        /// <returns>True if the path is absolute.</returns>
        protected internal virtual bool IsPathRooted(string path)
        {
            // We consider paths to be rooted if they start with '<<VolumeLetter>>:' and do
            // not start with '\' or '/'. In those cases, even that the paths are 'traditionally'
            // rooted, we consider them to be relative.
            // In Unix rooted paths start with '/' which is not supported by this action result
            // by default.

            return Path.IsPathRooted(path) && (IsNetworkPath(path) || !StartsWithForwardOrBackSlash(path));
        }

        private static bool StartsWithForwardOrBackSlash(string path)
        {
            return path.StartsWith("/", StringComparison.Ordinal) ||
                path.StartsWith("\\", StringComparison.Ordinal);
        }

        private static bool IsNetworkPath(string path)
        {
            return path.StartsWith("//", StringComparison.Ordinal) ||
                path.StartsWith("\\\\", StringComparison.Ordinal);
        }

        public string ReadFile(string filename)
        {
            FileProvider = GetFileSystem(services);
            var path = NormalizePath(filename);
            var fileInfo = FileProvider.GetFileInfo(path);
            using (var content = fileInfo.CreateReadStream())
            using (var reader = new StreamReader(content))
            {
                var fileContent = reader.ReadToEnd();
                return fileContent;
            }
        }

        private static readonly object locker = new object();

        public void WriteToFile(string filename, string content)
        {
            lock (locker)
            {
                var path = ResolveFilePath(FileProvider, filename);
                File.WriteAllText(path, content);
            }
        }
    }
}