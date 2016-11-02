namespace Contracts.Services.Infrastructure.DataExport
{
    public interface IDataExporter
    {
        ExportSummary Export(object[] objects);
    }
}
