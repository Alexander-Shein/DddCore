using System.IO;
using Contracts.Services.Infrastructure.Files.Services;

namespace Contracts.Services.Infrastructure.Files.Models.Input
{
    public class FileDetails
    {
        public Stream File { get; set; }
        public string OriginalFileName { get; set; }
        public string Location { get; set; }
        public StorageType StorageType { get; set; }
    }
}
