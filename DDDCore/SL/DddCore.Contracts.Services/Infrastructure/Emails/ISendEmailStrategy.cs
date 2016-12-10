using System.Threading.Tasks;

namespace DddCore.Contracts.Services.Infrastructure.Emails
{
    public interface ISendEmailStrategy
    {
        Task SendAsync(EmailDetails email);
    }
}
