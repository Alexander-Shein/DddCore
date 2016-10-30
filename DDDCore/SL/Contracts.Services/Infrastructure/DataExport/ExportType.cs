using System.ComponentModel;

namespace Contracts.Services.Infrastructure.DataExport
{
    public enum ExportType
    {
        [Description("text/csv")]
        Csv,

        [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        Excel
    }
}