using System.Data;

namespace Contracts.Services.Infrastructure.DataExport
{
    public interface IDataExporter
    {
        ExportSummary Export(IDataReader dataReader);

        string ContentType { get; }
    }
}
