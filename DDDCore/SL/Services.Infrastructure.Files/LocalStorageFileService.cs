using System;
using System.IO;
using Contracts.Crosscutting.Configuration;
using Contracts.Services.Infrastructure.Files.Models.Input;
using Contracts.Services.Infrastructure.Files.Models.View;

namespace Services.Infrastructure.Files
{
    public class LocalStorageFileService : FileServiceBase
    {
        public string ServerPath { get; set; }
        public IConfig Config { get; set; }

        public override string StorageType => "LocalStorage";

        public override FileSummary Upload(FileDetails fileDetails)
        {
            string location =
                Config
                    .Get<string>(String.Concat(StorageType, "_", fileDetails.LocationType));

            string uniqueFileName =
                Guid.NewGuid() + Path.GetExtension(fileDetails.OriginalFileName);

            string folderPath = Path.Combine(ServerPath, location);

            CreateFolder(folderPath);

            using (var fileStream = File.Create(Path.Combine(folderPath, uniqueFileName)))
            {
                fileDetails.File.CopyTo(fileStream);
            }

            return new FileSummary
            {
                StorageType = StorageType,
                FileName = uniqueFileName,
                Location = location
            };
        }

        public override Uri GetUri(FileSummary fileSummary)
        {
            string serverUrl =
                Config
                    .Get<string>(String.Concat(StorageType, "_ServerUrl"));

            return new Uri(String.Concat(serverUrl, fileSummary.Location, "/", fileSummary.FileName));
        }

        public override Stream Read(FileSummary fileSummary)
        {
            var filePath = Path.Combine(ServerPath, fileSummary.Location, fileSummary.FileName);
            return File.OpenRead(filePath);
        }

        #region Private Methods

        static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        #endregion
    }
}
