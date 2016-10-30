using System.IO;

namespace Contracts.Services.Infrastructure.Files.Models.Input
{
    public class FileDetails
    {
        public Stream File { get; set; }
        public string OriginalFileName { get; set; }
        public string LocationType { get; set; }

        public string GetExtension()
        {
            return Path.GetExtension(OriginalFileName);
        }
    }
}
