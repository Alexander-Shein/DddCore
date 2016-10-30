using System.IO;
using System.Linq;
using Contracts.Services.Infrastructure.DataExport;
using Crosscutting.Infrastructure;

namespace Services.Infrastructure.DataExport.Csv
{
    public class CsvDataExporter : DataExporterBase
    {
        #region Public Methods

        public override ExportSummary Export(object[] objects)
        {
            if (objects == null || !objects.Any())
                return null;

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            var header = GetHeaders(objects.FirstOrDefault()).Select(x => $"\"{x}\"");

            writer.WriteLine(header);

            foreach (var row in objects.Select(x => GetRow(x).Select(c => $"\"{c}\"")))
            {
                writer.WriteLine(row);
            }

            writer.Flush();
            stream.Position = 0;

            return new ExportSummary
            {
                File = stream,
                ContentType = ExportType.Csv.GetDescription()
            };
        }

        #endregion
    }
}