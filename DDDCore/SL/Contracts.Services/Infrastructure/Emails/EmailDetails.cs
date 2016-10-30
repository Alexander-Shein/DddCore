using System.Collections.Generic;
using Contracts.Services.Infrastructure.Files.Models.View;

namespace Contracts.Services.Infrastructure.Emails
{
    public class EmailDetails
    {
        public string MailTo { get; set; }
        public string MailFrom { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public FileSummary[] Attachments { get; set; }
    }
}