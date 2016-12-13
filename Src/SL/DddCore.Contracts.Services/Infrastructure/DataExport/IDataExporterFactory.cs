namespace DddCore.Contracts.Services.Infrastructure.DataExport
{
    public interface IDataExporterFactory
    {
        IDataExporter CreateDataExporter(ExportType type);
    }
}