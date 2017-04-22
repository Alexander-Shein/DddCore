namespace DddCore.Contracts.DAL
{
    public class ConnectionStrings
    {
        /// <summary>
        ///  DataBase connection string for write operations
        /// </summary>
        public string Oltp { get; set; }

        /// <summary>
        /// Connection string to readonly DataBase
        /// </summary>
        public string ReadOnly { get; set; }
    }
}