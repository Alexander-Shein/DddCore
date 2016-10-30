using System.Threading.Tasks;

namespace Contracts.Services.Application.DomainStack
{
    public interface ICreate<TVm, in TIm>
    {
        Task<TVm> CreateAsync(TIm im);
    }
}
