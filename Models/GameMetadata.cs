using System;

namespace Qrnick.FileServer.Models
{
    public class GameMetadata
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime UploadedAt { get; set; }
        public string GameId { get; set; }
        public string Description { get; set; }
    }
}