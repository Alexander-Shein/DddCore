using System.IO;
using DddCore.Contracts.Services.Infrastructure.Preview.Models;

namespace DddCore.Contracts.Services.Infrastructure.Preview
{
    public interface IPreviewService
    {
        PreviewSummary GeneratePreview(Stream file, int maxWidth, int maxHeight);
    }
}
