using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Qrnick.FileServer.Extensions;
using Qrnick.FileServer.Models;

namespace Qrnick.FileServer.Services
{
    public class UnityFilesService : IUnityFilesService
    {
        private readonly DirectoryInfo _unityFilesDir;
        public UnityFilesService(IConfiguration configuration)
        {
            var unityFilesDir = new DirectoryInfo(configuration.GetUnityFilesPath());
            if (unityFilesDir == null || !unityFilesDir.Exists)
                throw new DirectoryNotFoundException();

            this._unityFilesDir = unityFilesDir;
        }

        public List<GameMetadata> GetAvailableGames()
        {
            var gamesDictionary = _unityFilesDir.GetFiles().First(x => x.Name == "gamesDictionary.json");
            var jsonText = File.ReadAllText(gamesDictionary.FullName);
            var metadata = JsonSerializer.Deserialize<GameMetadata[]>(jsonText);
            return metadata.ToList();
        }

        public PhysicalFileResult GetDataFile(string gameId)
        {
            var dataFileName = $"{gameId}.data.gz";
            var matchingFile = _unityFilesDir.GetFiles().First(x => x.Name == dataFileName);
            return new PhysicalFileResult(matchingFile.FullName, "application/octet-stream");
        }

        public PhysicalFileResult GetFrameworkFile(string gameId)
        {
            var frameworkFileName = $"{gameId}.framework.js.gz";
            var matchingFile = _unityFilesDir.GetFiles().First(x => x.Name == frameworkFileName);
            return new PhysicalFileResult(matchingFile.FullName, "application/javascript");

        }

        public PhysicalFileResult GetLoaderFile(string gameId)
        {
            var loaderFileName = $"{gameId}.loader.js";
            var matchingFile = _unityFilesDir.GetFiles().First(x => x.Name == loaderFileName);
            return new PhysicalFileResult(matchingFile.FullName, "application/javascript");
        }

        public PhysicalFileResult GetWasmFile(string gameId)
        {
            var wasmFileName = $"{gameId}.wasm.gz";
            var matchingFile = _unityFilesDir.GetFiles().First(x => x.Name == wasmFileName);
            return new PhysicalFileResult(matchingFile.FullName, "application/wasm");
        }
    }
}