using Contracts.Services.Infrastructure.Files.Models.View;
using Contracts.Services.Infrastructure.Preview.Models;

namespace Contracts.Services.Infrastructure.Preview.Services
{
    public interface IPreviewService
    {
        PreviewSummary GeneratePreview(FileSummary fileSummary, PreviewType size);
        PreviewSummaries GeneratePreviews(FileSummary file, PreviewType size);
    }
}
