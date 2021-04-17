using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Qrnick.FileServer.Services
{
    public class GamesMetadataHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<GamesMetadataHostedService> _logger;
        private readonly GamesMetadataStorage _metadataStorage;
        private Timer _timer;

        public GamesMetadataHostedService(ILogger<GamesMetadataHostedService> logger, GamesMetadataStorage metadataStorage)
        {
            _logger = logger;
            _metadataStorage = metadataStorage;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GamesMetadata Hosted Service is starting");
            _timer = new Timer(ScanFolder, null, TimeSpan.Zero.Milliseconds, TimeSpan.FromHours(1).Milliseconds);
            return Task.CompletedTask;
        }

        private void ScanFolder(object state)
        {
            _metadataStorage.ScanFolderForGames();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GamesMetadata Hosted Service is stopping");
            _timer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}