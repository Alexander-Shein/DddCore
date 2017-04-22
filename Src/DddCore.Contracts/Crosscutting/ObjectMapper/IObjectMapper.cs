namespace DddCore.Contracts.Crosscutting.ObjectMapper
{
    public interface IObjectMapper
    {
        /// <summary>
        /// Maps object to new object with generic type.
        /// Wrapper for object mappers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        T Map<T>(object from);
    }
}