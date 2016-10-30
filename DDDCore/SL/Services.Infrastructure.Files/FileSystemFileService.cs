using System;
using System.Collections.Generic;
using System.IO;
using Contracts.Crosscutting.Configuration;
using Contracts.Services.Infrastructure.Files.Models.Input;
using Contracts.Services.Infrastructure.Files.Models.View;
using Contracts.Services.Infrastructure.Files.Services;
using Contracts.Services.Infrastructure.Files.Services.Validation;

namespace Services.Infrastructure.Files
{
    public class FileSystemFileService : FileServiceBase
    {
        #region Private Members

        readonly string serverPath;
        readonly IConfig config;

        #endregion

        public FileSystemFileService(IDictionary<Restrictions, IFileValidator> validators, IConfig config) : base(validators)
        {
            this.config = config;
        }

        #region Public Methods

        public override FileSummary Upload(FileDetails fileDetails)
        {
            string location =
                config
                    .Get<string>(String.Concat(StorageType.FileSystem, "_", fileDetails.Location));

            string uniqueFileName =
                Guid.NewGuid() + Path.GetExtension(fileDetails.OriginalFileName);

            string folderPath = Path.Combine(serverPath, location);

            CreateFolder(folderPath);

            using (var fileStream = File.Create(Path.Combine(folderPath, uniqueFileName)))
            {
                fileDetails.File.CopyTo(fileStream);
            }

            return new FileSummary
            {
                StorageType = StorageType.FileSystem,
                FileName = uniqueFileName,
                Location = location
            };
        }

        public override Uri GetUri(FileSummary fileSummary)
        {
            string serverUrl =
                config
                    .Get<string>(String.Concat(StorageType.FileSystem, "_ServerUrl"));

            return new Uri(String.Concat(serverUrl, fileSummary.Location, "/", fileSummary.FileName));
        }

        public override Stream Read(FileSummary fileSummary)
        {
            var filePath = Path.Combine(serverPath, fileSummary.Location, fileSummary.FileName);
            return File.OpenRead(filePath);
        }

        #endregion

        #region Private Methods

        static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        #endregion
    }
}
