using System;
using System.IO;
using DddCore.Contracts.Services.Infrastructure.Files.Models.Input;
using DddCore.Contracts.Services.Infrastructure.Files.Models.View;
using DddCore.Contracts.Services.Infrastructure.Files.Validation;

namespace DddCore.Contracts.Services.Infrastructure.Files
{
    public interface IFileService
    {
        FileSummary Upload(FileDetails fileDetails, Restrictions restrictions);
        FileSummary Upload(FileDetails fileDetails);

        Uri GetUri(FileSummary fileSummary);
        Stream Read(FileSummary fileSummary);
    }
}
