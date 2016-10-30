namespace Contracts.Services.Infrastructure.Emails
{
    public interface IEmailService
    {
        void Send(EmailDetails email);
    }
}
