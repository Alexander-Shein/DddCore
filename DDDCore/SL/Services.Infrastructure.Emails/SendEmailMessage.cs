using System;
using Contracts.Services.Infrastructure.Files.Models.View;
using Contracts.Services.Infrastructure.ServiceBus;

namespace Services.Infrastructure.Emails
{
    public class SendEmailMessage : IApplicationMessage
    {
        private const string DefaultEventType = "SendEmailEvent";

        public SendEmailMessage(DateTime dateTimeEventOccurred, string mailTo, string mailFrom, string body, string subject)
        {
            DateTimeEventOccurred = dateTimeEventOccurred;
            MailTo = mailTo;
            MailFrom = mailFrom;
            Body = body;
            Subject = subject;
            EventType = DefaultEventType;
        }

        public DateTime DateTimeEventOccurred { get; private set; }
        public string EventType { get; private set; }

        public string MailTo { get; private set; }
        public string MailFrom { get; private set; }
        public string Body { get; private set; }
        public string Subject { get; private set; }

        public FileSummary[] Attachments { get; set; }
    }
}