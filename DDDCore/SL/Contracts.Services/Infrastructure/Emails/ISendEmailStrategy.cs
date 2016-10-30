using System.Threading.Tasks;

namespace Contracts.Services.Infrastructure.Emails
{
    public interface ISendEmailStrategy
    {
        Task SendAsync(EmailDetails email);
    }
}
