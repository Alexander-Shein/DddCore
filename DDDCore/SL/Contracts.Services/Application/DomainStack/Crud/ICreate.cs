using System.Threading.Tasks;

namespace Contracts.Services.Application.DomainStack.Crud
{
    public interface ICreate<TVm, in TIm>
    {
        Task<TVm> CreateAsync(TIm im);
    }
}
