namespace Contracts.Services.Infrastructure.DataExport
{
    public interface IDataExporter
    {
        //ExportResult Export(IDataReader dataReader);
        ExportSummary Export(object[] objects);
    }
}
