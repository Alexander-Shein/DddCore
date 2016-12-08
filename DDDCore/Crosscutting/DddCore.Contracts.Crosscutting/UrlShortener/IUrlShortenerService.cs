using System.Threading.Tasks;

namespace DddCore.Contracts.Crosscutting.UrlShortener
{
    public interface IUrlShortenerService
    {
        Task<string> GetShortUrlAsync(string longUrl);
    }
}
