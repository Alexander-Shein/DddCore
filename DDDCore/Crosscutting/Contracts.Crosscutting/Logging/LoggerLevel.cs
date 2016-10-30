using System.ComponentModel;

namespace Contracts.Crosscutting.Logging
{
    public enum LoggerLevel
    {
        [Description("Trace")]
        Trace,

        [Description("Debug")]
        Debug,

        [Description("Information")]
        Information,

        [Description("Warning")]
        Warning,

        [Description("Error")]
        Error,

        [Description("Fatal")]
        Fatal
    }
}