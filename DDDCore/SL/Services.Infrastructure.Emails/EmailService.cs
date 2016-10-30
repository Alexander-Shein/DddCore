using System;
using Contracts.Services.Infrastructure.Emails;
using Contracts.Services.Infrastructure.ServiceBus;

namespace Services.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        public IServiceBus ServiceBus { get; set; }
        public ITemplateReader TemplateReader { get; set; }

        public void Send(EmailDetails email)
        {
            var tmpl = TemplateReader.Read(email.TmplName);
            var body = tmpl.Tmpl;

            foreach (var parameter in email.Parameters)
            {
                body = body.Replace(String.Concat("{", parameter.Key, "}"), parameter.Value);
            }

            var emailMessage = new SendEmailMessage(DateTime.UtcNow, email.MailTo, email.MailFrom, body, tmpl.Subject)
            {
                Attachments = email.Attachments
            };

            ServiceBus.SendLocal(emailMessage);
        }
    }
}
