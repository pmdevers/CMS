using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

using Panther.CMS.Services.Media;

namespace Panther.CMS.Controllers.Api
{
    [Route("api/media")]
    public class MediaController : Controller
    {
        private readonly IMediaService mediaService;

        public MediaController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        [Route("download/{name}")]
        public ActionResult Get(string name)
        {
            var media = mediaService.Get(name);
            //Response.Headers.Add("Cache-Control", new[] { "max-age=290304000", "public" });
            return File(media.Path, media.Type, Path.GetFileName(media.Path));
        }

        [Route("{width}/{height}/{name}")]
        public ActionResult Get(int width, int height, string name)
        {
            var media = mediaService.Get(name);
            Response.Headers.Add("Cache-Control", media.CacheControl.Split(new [] {','}));
            return File(media.Path, media.Type);
        }
            

        [HttpPost]
        public ActionResult UploadFile(IFormFile file, string name, string description)
        {
            if (file != null && file.Length > 0)
            {
                mediaService.SaveMedia(file, name, description);
                return Json(new { Status = "Ok" });
            }
            return Json(new { Status = "Error" });
        } 
    }
}
