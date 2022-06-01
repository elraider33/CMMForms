using Library.Application.Data.Files.Queries;
using Library.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FilesController : BaseController
    {
        private ILogger<FilesController> _logger;

        public FilesController(ILogger<FilesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            _logger.LogInformation("[controller] Get File by id");
            var file = await Sender.Send(new GetFileById() { Id = id });
            //try
            //{
            //    var address = new Uri($"http://ec2-3-92-156-177.compute-1.amazonaws.com/gridfs/fs/key/{file.FilesId}?download=true");
            //    var address = new Uri($"http://ec2-3-92-156-177.compute-1.amazonaws.com/gridfs/fs/key/5660534fa02a650268cab094?download=true");
            //    _logger.LogInformation($"[controller] file address {address.AbsoluteUri}");
            //    using (WebClient client = new WebClient())
            //    using (Stream stream = client.OpenRead(address))
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        _logger.LogInformation($"[controller] downloading the file from {address.AbsoluteUri}");
            //        stream.CopyTo(ms);
            //        _logger.LogInformation("ms length");
            //        _logger.LogInformation(ms.Length.ToString());
            //        return File(ms.ToArray(), "application/pdf", file.Filename);
            //    }
            //}
            //catch (Exception e)
            //{
            //    _logger.LogError("there was an error trying to download the file");
            //    _logger.LogError(e.Message);
            //    throw;
            //}
            var mbSize = ConvertHelper.ConvertBytesToMegaBytes((long)file.Size);
            if(mbSize < 50)
            {
                var fileData = await Sender.Send(new GetDownloadFileById() { Id = file.FilesId, Name = file.Filename });
                return File(fileData, "application/pdf", file.Filename);
            }
            var stream = await Sender.Send(new GetDownloadStreamFileById() { Id = file.FilesId, Name = file.Filename });
            _logger.LogInformation("[controller] getting the stream");
            _logger.LogInformation(stream.Length.ToString());
            return File(stream.ToArray(), "application/pdf", file.Filename);
        }
    }
}
