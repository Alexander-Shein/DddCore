namespace DddCore.Contracts.Crosscutting.ObjectMapper
{
    public interface IObjectMapper
    {
        /// <summary>
        /// Maps object from passed to new object with generic type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        T Map<T>(object from);
    }
}