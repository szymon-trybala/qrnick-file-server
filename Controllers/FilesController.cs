using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Qrnick.FileServer.Models;
using Qrnick.FileServer.Services;

namespace Qrnick.FileServer.Controllers
{
    [ApiController]
    [Route("game")]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<FilesController> _logger;
        private readonly IUnityFilesService _unityFilesService;
        private readonly GamesMetadataStorage _metadataStorage;
        public FilesController(ILogger<FilesController> logger, IUnityFilesService unityFilesService, GamesMetadataStorage metadataStorage)
        {
            _unityFilesService = unityFilesService;
            _logger = logger;
            _metadataStorage = metadataStorage;
        }

        [HttpGet("List")]
        public List<GameMetadata> GetAvailableGames()
        {
            return _metadataStorage.GetAvailableGames();
        }

        [HttpGet("UnityDataFile")]
        public FileResult GetDataFile(string gameId)
        {
            try
            {
                this.HttpContext.Response.Headers.Add("Content-Type", "application/octet-stream");
                this.HttpContext.Response.Headers.Add("Content-Encoding", "gzip");
                return _unityFilesService.GetDataFile(gameId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Couldn't get data file of game with if {gameId}: \n{e}");
                HttpContext.Response.StatusCode = 500;
                return null;
            }
        }

        [HttpGet("UnityFrameworkFile")]
        public FileResult GetFrameworkFile(string gameId)
        {
            try
            {
                this.HttpContext.Response.Headers.Add("Content-Type", "application/javascript");
                this.HttpContext.Response.Headers.Add("Content-Encoding", "gzip");
                return _unityFilesService.GetFrameworkFile(gameId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Couldn't get framework file of game with if {gameId}: \n{e}");
                HttpContext.Response.StatusCode = 500;
                return null;
            }
        }

        [HttpGet("UnityLoaderFile")]
        public FileResult GetLoaderFile(string gameId)
        {
            try
            {
                this.HttpContext.Response.Headers.Add("Content-Type", "application/javascript");
                return _unityFilesService.GetLoaderFile(gameId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Couldn't get loader file of game with if {gameId}: \n{e}");
                HttpContext.Response.StatusCode = 500;
                return null;
            }
        }

        [HttpGet("UnityWasmFile")]
        public FileResult GetWasmFile(string gameId)
        {
            try
            {
                this.HttpContext.Response.Headers.Add("Content-Type", "application/wasm");
                this.HttpContext.Response.Headers.Add("Content-Encoding", "gzip");
                return _unityFilesService.GetWasmFile(gameId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Couldn't get framework file of game with if {gameId}: \n{e}");
                HttpContext.Response.StatusCode = 500;
                return null;
            }
        }
    }
}
