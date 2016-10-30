using System;
using System.IO;
using Contracts.Services.Infrastructure.Files.Models.Input;
using Contracts.Services.Infrastructure.Files.Models.View;
using Contracts.Services.Infrastructure.Files.Services.Validation;

namespace Contracts.Services.Infrastructure.Files.Services
{
    public interface IFileService : IFileUploadService<FileSummary>
    {
        string StorageType { get; }

        FileSummary Upload(FileDetails fileDetails, Restrictions restrictions);

        Uri GetUri(FileSummary fileSummary);
        Stream Read(FileSummary fileSummary);
    }
}
