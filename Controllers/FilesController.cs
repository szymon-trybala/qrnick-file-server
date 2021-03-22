using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Qrnick.FileServer.Models;
using Qrnick.FileServer.Services;

namespace Qrnick.FileServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IUnityFilesService _unityFilesService;
        public FilesController(IUnityFilesService unityFilesService)
        {
            _unityFilesService = unityFilesService;
        }

        [HttpGet("FilesList")]
        public List<FileDto> GetAvailableFiles()
        {
            return _unityFilesService.GetAvailableFiles();
        }

        [HttpGet("Data")]
        public FileResult GetDataFile(string gameId)
        {
            var contentDisposition = new ContentDisposition
            {
                FileName = $"{gameId}.data.gz",
                Inline = false,
            };
            this.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            this.HttpContext.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return _unityFilesService.GetDataFile(gameId);
        }

        [HttpGet("Framework")]
        public FileResult GetFrameworkFile(string gameId)
        {
            var contentDisposition = new ContentDisposition
            {
                FileName = $"{gameId}.framework.js.gz",
                Inline = false,
            };
            this.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            this.HttpContext.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return _unityFilesService.GetFrameworkFile(gameId);
        }

        [HttpGet("Loader")]
        public FileResult GetLoaderFile(string gameId)
        {
            var contentDisposition = new ContentDisposition
            {
                FileName = $"{gameId}.loader.js",
                Inline = false,
            };
            this.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            this.HttpContext.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return _unityFilesService.GetLoaderFile(gameId);
        }

        [HttpGet("wasm")]
        public FileResult GetWasmFile(string gameId)
        {
            var contentDisposition = new ContentDisposition
            {
                FileName = $"{gameId}.wasm.gz",
                Inline = false,
            };
            this.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            this.HttpContext.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return _unityFilesService.GetWasmFile(gameId);
        }
    }
}
