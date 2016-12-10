using System.IO;

namespace DddCore.Contracts.Services.Infrastructure.Emails
{
    public class EmailAttachmentDetails
    {
        public Stream File { get; set; }

        public string FileName { get; set; }
    }
}