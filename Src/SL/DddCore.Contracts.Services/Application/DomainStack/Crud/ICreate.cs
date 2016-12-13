using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface ICreate<TVm, in TIm>
    {
        Task<TVm> CreateAsync(TIm im);
    }
}
