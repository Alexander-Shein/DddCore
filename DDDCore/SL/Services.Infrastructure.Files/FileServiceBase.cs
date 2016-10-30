using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Contracts.Services.Infrastructure.Files.Models.Input;
using Contracts.Services.Infrastructure.Files.Models.View;
using Contracts.Services.Infrastructure.Files.Services;
using Contracts.Services.Infrastructure.Files.Services.Validation;

namespace Services.Infrastructure.Files
{
    public abstract class FileServiceBase : IFileService
    {
        public IDictionary<Restrictions, IFileValidator> Validators { get; set; }

        public abstract string StorageType { get; }
        public abstract FileSummary Upload(FileDetails fileDetails);
        public abstract Uri GetUri(FileSummary fileSummary);
        public abstract Stream Read(FileSummary fileSummary);

        public FileSummary Upload(FileDetails fileDetails, Restrictions restrictions)
        {
            var availableSuits = Enum.GetValues(typeof(Restrictions)).Cast<Enum>();
            foreach (Restrictions restriction in availableSuits.Where(restrictions.HasFlag))
            {
                var validator = Validators[restriction];
                validator.Validate(fileDetails);

                fileDetails.File.Position = 0;
            }

            return Upload(fileDetails);
        }
    }
}