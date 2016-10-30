using System;
using Contracts.Services.Infrastructure.DataExport;
using Services.Infrastructure.DataExport.Csv;
using Services.Infrastructure.DataExport.Excel;

namespace Services.Infrastructure.DataExport
{
    public class DataExporterFactory : IDataExporterFactory
    {
        public IDataExporter CreateDataExporter(ExportType type)
        {
            switch (type)
            {
                case ExportType.Csv:
                    return new CsvDataExporter();

                case ExportType.Excel:
                    return new ExcelDataExporter();

                default:
                    throw new ArgumentException(nameof(type));
            }
        }
    }
}