namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface ICreate<TVm, in TIm>
    {
        TVm Create(TIm im);
    }
}
