namespace Contracts.Crosscutting.IoC
{
    public interface IIocModule
    {
        void Install(IRegistrationContainer container);
    }
}