namespace DddCore.Contracts.BLL.Errors
{
    public class Error
    {
        public Error()
        {
        }

        public Error(int code, string description)
        {
            Code = code;
            Description = description;
        }

        public int Code { get; set; }
        public string Description { get; set; }
    }
}