using System.Threading.Tasks;

namespace Contracts.Crosscutting.UrlShortener
{
    public interface IUrlShortenerService
    {
        Task<string> GetShortUrlAsync(string longUrl);
    }
}
