using Contracts.Services.Infrastructure.Files.Models.Input;

namespace Contracts.Services.Infrastructure.Files.Validation
{
    public interface IFileValidator
    {
        void Validate(FileDetails fileDetails);
    }
}