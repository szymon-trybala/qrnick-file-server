using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Qrnick.FileServer.Models;
using Qrnick.FileServer.Services;

namespace Qrnick.FileServer.Controllers
{
    [ApiController]
    [Route("game")]
    public class FilesController : ControllerBase
    {
        private readonly IUnityFilesService _unityFilesService;
        public FilesController(IUnityFilesService unityFilesService)
        {
            _unityFilesService = unityFilesService;
        }

        [HttpGet("List")]
        public List<GameMetadata> GetAvailableGames()
        {
            return _unityFilesService.GetAvailableGames();
        }

        [HttpGet("UnityDataFile")]
        public FileResult GetDataFile(string gameId)
        {
            this.HttpContext.Response.Headers.Add("Content-Type", "application/octet-stream");
            return _unityFilesService.GetDataFile(gameId);
        }

        [HttpGet("UnityFrameworkFile")]
        public FileResult GetFrameworkFile(string gameId)
        {
            this.HttpContext.Response.Headers.Add("Content-Type", "application/javascript");
            return _unityFilesService.GetFrameworkFile(gameId);
        }

        [HttpGet("UnityLoaderFile")]
        public FileResult GetLoaderFile(string gameId)
        {
            return _unityFilesService.GetLoaderFile(gameId);
        }

        [HttpGet("UnityWasmFile")]
        public FileResult GetWasmFile(string gameId)
        {
            this.HttpContext.Response.Headers.Add("Content-Type", "application/wasm");
            return _unityFilesService.GetWasmFile(gameId);
        }
    }
}
