using Contracts.Services.Infrastructure.Files.Models.Input;

namespace Contracts.Services.Infrastructure.Files.Services
{
    public interface IFileUploadService<out TResult>
    {
        TResult Upload(FileDetails fileDetails);
    }
}