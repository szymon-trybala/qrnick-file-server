using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Qrnick.FileServer.Extensions;
using Qrnick.FileServer.Helpers;
using Qrnick.FileServer.Models;

namespace Qrnick.FileServer.Services
{
    public class GamesMetadataStorage
    {
        private readonly ILogger<GamesMetadataStorage> _logger;
        private readonly string _gamesFolderPath;
        private List<GameMetadata> GamesMetadata;

        public GamesMetadataStorage(ILogger<GamesMetadataStorage> logger, IConfiguration configuration)
        {
            _logger = logger;
            _gamesFolderPath = configuration.GetUnityFilesPath();
            GamesMetadata = new List<GameMetadata>();
        }

        public List<GameMetadata> GetAvailableGames()
        {
            return GamesMetadata;
        }

        public void ScanFolderForGames()
        {
            try
            {
                var unityFilesDir = new DirectoryInfo(_gamesFolderPath);
                if (unityFilesDir == null || !unityFilesDir.Exists)
                    throw new DirectoryNotFoundException();
                var files = unityFilesDir.GetFiles();
                var fileGroups = GroupFilesByGameId(files);
                GamesMetadata = GetGamesMetadataFromGroupedFiles(fileGroups);
                _logger.LogInformation($"Scanned for games, found {GamesMetadata.Count} games");

            }
            catch (Exception e)
            {
                _logger.LogError($"Couldn't scan folder for game files: \n{e}");
            }
        }

        private Dictionary<(string, int, int), List<FileInfo>> GroupFilesByGameId(FileInfo[] files)
        {
            var fileGroups = new Dictionary<(string, int, int), List<FileInfo>>();
            foreach (var file in files)
            {
                try
                {
                    var pathSplitted = file.Name.Split(".");
                    if (file.Extension == ".br" || file.Extension == ".gz" || file.Extension == ".js")
                    {
                        List<FileInfo> existing;
                        var gameId = (pathSplitted[0], Int32.Parse(pathSplitted[1]), Int32.Parse(pathSplitted[2]));
                        if (!fileGroups.TryGetValue(gameId, out existing))
                        {
                            existing = new List<FileInfo>();
                            fileGroups[gameId] = existing;
                        }
                        existing.Add(file);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"Couldn't check if file {file.Name} is game file, skipping: \n{e}");
                }
            }
            return fileGroups;
        }

        private List<GameMetadata> GetGamesMetadataFromGroupedFiles(Dictionary<(string, int, int), List<FileInfo>> groups)
        {
            var games = new List<GameMetadata>();
            var knownGames = KnownGames.GetDictionary();
            foreach (var fileGroup in groups)
            {
                try
                {
                    if (fileGroup.Value.Count == 4)
                    {
                        (string, string) knownGameInfo;
                        knownGames.TryGetValue(fileGroup.Key.Item1, out knownGameInfo);
                        games.Add(new GameMetadata()
                        {
                            GameId = $"{fileGroup.Key.Item1}.{fileGroup.Key.Item2}.{fileGroup.Key.Item3}",
                            Version = $"{fileGroup.Key.Item2}.{fileGroup.Key.Item3}",
                            UploadedAt = fileGroup.Value.First().LastWriteTimeUtc,
                            Name = knownGameInfo.Item1 == null ? fileGroup.Key.Item1 : knownGameInfo.Item1,
                            Description = knownGameInfo.Item2 == null ? "" : knownGameInfo.Item2,
                        });
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"Couldn't convert file group to game metadata object, skipping: \n{e}");
                }
            }
            return games;
        }
    }
}