namespace DddCore.Contracts.Dal
{
    public class ConnectionStrings
    {
        /// <summary>
        /// Connection string to DataBase for write operations
        /// </summary>
        public string Oltp { get; set; }

        /// <summary>
        /// Connection string to readonly DataBase
        /// </summary>
        public string ReadOnly { get; set; }
    }
}