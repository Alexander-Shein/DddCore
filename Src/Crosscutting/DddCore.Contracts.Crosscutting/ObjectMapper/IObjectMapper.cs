namespace DddCore.Contracts.Crosscutting.ObjectMapper
{
    public interface IObjectMapper
    {
        T Map<T>(object from);
    }
}