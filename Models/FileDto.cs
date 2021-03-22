using System;

namespace Qrnick.FileServer.Models 
{
    public class FileDto
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public long Size { get; set; }
    }
}