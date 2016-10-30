using System.IO;

namespace Contracts.Services.Infrastructure.DataExport
{
    public class ExportSummary
    {
        public Stream File { get; set; }
        public string ContentType { get; set; }
    }
}
