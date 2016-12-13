using DddCore.Contracts.Services.Infrastructure.Files.Models.Input;

namespace DddCore.Contracts.Services.Infrastructure.Files.Validation
{
    public interface IFileValidator
    {
        void Validate(FileDetails fileDetails);
    }
}