using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;

using Panther.CMS.Interfaces;
using Panther.CMS.Storage.Media;

namespace Panther.CMS.Services.Media
{
    public class MediaService : BaseService, IMediaService
    {
        readonly IMediaStore mediaStore;
        readonly IHostingEnvironment environment;
        public MediaService(IPantherContext context, IMediaStore store, IHostingEnvironment environment) : base(context)
        {
            mediaStore = store;
            this.environment = environment;
        }

        public void SaveMedia(IFormFile file, string name, string description)
        {
            if (file.Length > 0)
            {
                var targetDirectory = Path.Combine(environment.WebRootPath, "Media");
                var fileName = GetFileName(file);
                var savePath = Path.Combine(targetDirectory, fileName);
                file.SaveAs(savePath);

                var media = mediaStore.FindAll(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault() ?? new Entities.Media();

                media.Name = name;
                media.Description = description;
                media.Type = file.ContentType;
                media.Path = "/media/" + fileName;
                
                mediaStore.Add(media);
            }
        }

        public Entities.Media Get(string name)
        {
            var media = mediaStore.FindAll(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
            
            return media;
        }

        private static string GetFileName(IFormFile file) => file.ContentDisposition.Split(';')
                                                                    .Select(x => x.Trim())
                                                                    .Where(x => x.StartsWith("filename="))
                                                                    .Select(x => x.Substring(9).Trim('"'))
                                                                    .First();

    }
}
