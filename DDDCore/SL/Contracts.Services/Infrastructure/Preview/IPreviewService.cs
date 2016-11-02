using System.IO;
using Contracts.Services.Infrastructure.Preview.Models;

namespace Contracts.Services.Infrastructure.Preview
{
    public interface IPreviewService
    {
        PreviewSummary GeneratePreview(Stream file, int maxWidth, int maxHeight);
    }
}
