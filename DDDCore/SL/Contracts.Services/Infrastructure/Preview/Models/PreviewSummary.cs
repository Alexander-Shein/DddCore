using System.IO;

namespace Contracts.Services.Infrastructure.Preview.Models
{
    public class PreviewSummary
    {
        public Stream File { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}