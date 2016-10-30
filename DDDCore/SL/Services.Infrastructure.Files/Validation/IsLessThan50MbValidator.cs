using System;
using Contracts.Services.Infrastructure.Files.Models.Input;
using Contracts.Services.Infrastructure.Files.Services.Validation;

namespace Services.Infrastructure.Files.Validation
{
    public class IsLessThan50MbValidator : IFileValidator
    {
        const long MaxFileSize = 52428800; // 50 megabytes

        public void Validate(FileDetails fileDetails)
        {
            if (fileDetails.File.Length > MaxFileSize)
            {
                throw new ArgumentException("File size should be less than 50 megabytes.");
            }
        }
    }
}