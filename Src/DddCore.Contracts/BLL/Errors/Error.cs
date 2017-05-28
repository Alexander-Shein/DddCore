namespace DddCore.Contracts.BLL.Errors
{
    public class Error
    {
        public Error()
        {
        }

        public Error(int code, string description, Severity severity = Severity.Error)
        {
            Code = code;
            Description = description;
            Severity = severity;
        }

        public int Code { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; set; } = Severity.Error;
    }
}