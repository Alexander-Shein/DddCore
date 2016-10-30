using Contracts.Services.Infrastructure.Files.Models.Input;

namespace Contracts.Services.Infrastructure.Files.Services.Validation
{
    public interface IFileValidator
    {
        void Validate(FileDetails fileDetails);
    }
}