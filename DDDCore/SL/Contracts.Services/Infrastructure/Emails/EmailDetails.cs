namespace Contracts.Services.Infrastructure.Emails
{
    public class EmailDetails
    {
        public string MailTo { get; set; }
        public string MailFrom { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailAttachmentDetails[] Attachments { get; set; }
    }
}