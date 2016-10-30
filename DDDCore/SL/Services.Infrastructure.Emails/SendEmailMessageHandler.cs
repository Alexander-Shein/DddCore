using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Contracts.Crosscutting.Configuration;
using Contracts.Crosscutting.Logging;
using Contracts.Domain.Events;
using Contracts.Services.Infrastructure.Files.Models.View;
using Contracts.Services.Infrastructure.Files.Services;

namespace Services.Infrastructure.Emails
{
    public class SendEmailMessageHandler : IHandle<SendEmailMessage>
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

        public IConfig Config { get; set; }
        public IFileService FileService { get; set; }
        public ILoggerFactory LoggerFactory { get; set; }

        public void Handle(SendEmailMessage email)
        {
            var client = CreateSmtpClient();
            var mail = CreateMailMessage(Config.Get<string>(Consts.Smtp.MailTo), email.MailFrom, email.Subject, email.Body);

            Attach(mail, email.Attachments);

            try
            {
                client.Send(mail);
            }
            catch (Exception e)
            {
                var logger = LoggerFactory.GetLogger("SendEmailMessageHandler");
                logger.LogError("SendEmail", e);

                throw;
            }
        }

        #region Private Methods

        void Attach(MailMessage mail, FileSummary[] attachments)
        {
            if (attachments == null) return;

            foreach (var fileSummary in attachments)
            {
                var stream = FileService.Read(fileSummary);

                Attachment attachment = new Attachment(stream, MediaTypeNames.Application.Pdf);
                ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = DateTime.UtcNow;
                disposition.ModificationDate = DateTime.UtcNow;
                disposition.ReadDate = DateTime.UtcNow;
                disposition.FileName = fileSummary.FileName;
                disposition.Size = stream.Length;
                disposition.DispositionType = DispositionTypeNames.Attachment;
                mail.Attachments.Add(attachment);
            }
        }

        SmtpClient CreateSmtpClient()
        {
            var port = Config.Get<int>(Consts.Smtp.Port);
            var host = Config.Get<string>(Consts.Smtp.Host);
            var username = Config.Get<string>(Consts.Smtp.Username);
            var password = Config.Get<string>(Consts.Smtp.Password);

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
            mailFrom = mailFrom ?? Config.Get<string>(Consts.Smtp.MailFrom);
            mailTo = mailTo ?? Config.Get<string>(Consts.Smtp.MailTo);

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