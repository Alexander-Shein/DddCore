namespace DddCore.Contracts.Services.Application.DomainStack.Crud
{
    public interface ICreateChild<TVm, in TParrentKey, in TIm>
    {
        TVm CreateChild(TParrentKey key, TIm im);
    }
}
