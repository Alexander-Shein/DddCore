using Contracts.Services.Infrastructure.Files.Models.View;

namespace Contracts.Services.Infrastructure.Preview.Models
{
    public class PreviewSummary
    {
        public FileSummary FileSummary { get; set; }
        public PreviewType Size { get; set; }
    }
}