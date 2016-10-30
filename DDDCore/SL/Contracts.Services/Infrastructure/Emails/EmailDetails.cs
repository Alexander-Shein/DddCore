using System.Collections.Generic;
using Contracts.Services.Infrastructure.Files.Models.View;

namespace Contracts.Services.Infrastructure.Emails
{
    public class EmailDetails
    {
        public string MailTo { get; set; }
        public string MailFrom { get; set; }
        public string TmplName { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
        public FileSummary[] Attachments { get; set; }
    }
}