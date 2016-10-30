using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using Contracts.Crosscutting.Configuration;
using Contracts.Services.Infrastructure.Emails;

namespace Services.Infrastructure.Emails
{
    public class SmtpSendEmailStrategy : ISendEmailStrategy
    {
        private static class Consts
        {
            public static class Smtp
            {
                public const string Port = "Smtp_Port";
                public const string Host = "Smtp_Host";
                public const string Username = "Smtp_Username";
                public const string Password = "Smtp_Password";
                public const string MailFrom = "Smtp_MailFrom";
                public const string MailTo = "Smtp_MailTo";
            }
        }

        #region Private Members

        readonly IConfig config;

        #endregion

        public SmtpSendEmailStrategy(IConfig config)
        {
            this.config = config;
        }

        public Task SendAsync(EmailDetails email)
        {
            var client = CreateSmtpClient();
            var mail = CreateMailMessage(email.MailTo, email.MailFrom, email.Subject, email.Body);

            Attach(mail, email.Attachments);

            client.SendAsync(mail, null);
            return Task.CompletedTask;
        }

        #region Private Methods

        void Attach(MailMessage mail, EmailAttachmentDetails[] attachments)
        {
            if (attachments == null) return;

            foreach (var file in attachments)
            {
                Attachment attachment = new Attachment(file.File, MimeMapping.GetMimeMapping(file.FileName));
                ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = DateTime.UtcNow;
                disposition.ModificationDate = DateTime.UtcNow;
                disposition.ReadDate = DateTime.UtcNow;
                disposition.FileName = file.FileName;
                disposition.Size = file.File.Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                mail.Attachments.Add(attachment);
            }
        }

        SmtpClient CreateSmtpClient()
        {
            var port = config.Get<int>(Consts.Smtp.Port);
            var host = config.Get<string>(Consts.Smtp.Host);
            var username = config.Get<string>(Consts.Smtp.Username);
            var password = config.Get<string>(Consts.Smtp.Password);

            SmtpClient client = new SmtpClient
            {
                Port = port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = host,
                EnableSsl = true,
                Credentials = new NetworkCredential(username, password)
            };

            return client;
        }

        MailMessage CreateMailMessage(string mailTo, string mailFrom, string subject, string body)
        {
            mailFrom = mailFrom ?? config.Get<string>(Consts.Smtp.MailFrom);
            mailTo = mailTo ?? config.Get<string>(Consts.Smtp.MailTo);

            MailMessage mail = new MailMessage(mailFrom, mailTo)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            return mail;
        }

        #endregion
    }
}