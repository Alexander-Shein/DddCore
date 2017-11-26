namespace DddCore.Contracts.BLL.Domain.BusinessRules
{
    public class BusinessError
    {
        public BusinessError()
        {
        }

        public BusinessError(string code, string description, Severity severity = Severity.Error)
        {
            Code = code;
            Description = description;
            Severity = severity;
        }

        public string Code { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; set; } = Severity.Error;

        public static BusinessError Create(string code, string description, Severity severity = Severity.Error)
        {
            return new BusinessError(code, description, severity);
        }
    }
}
