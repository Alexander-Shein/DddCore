using System;
using System.Threading.Tasks;
using Contracts.Crosscutting.UrlShortener;
using Google.Apis.Services;
using Google.Apis.Urlshortener.v1;
using Google.Apis.Urlshortener.v1.Data;

namespace Crosscutting.Infrastructure.UrlShortener
{
    public class UrlShortenerService : IUrlShortenerService
    {
        #region Private Members

        const string ApiKey = "AIzaSyBR2E9sxI7N8M6_iS2z15gnE08AaNL_3C0";

        #endregion

        #region Public Methods

        public async Task<string> GetShortUrlAsync(string longUrl)
        {
            var urlShortenerService = CreateUrlshortenerService();

            var shortUrl =
                await urlShortenerService
                    .Url
                    .Insert(new Url {LongUrl = longUrl})
                    .ExecuteAsync();

            return shortUrl == null ? String.Empty : shortUrl.Id;
        }

        #endregion

        #region Private Methods

        UrlshortenerService CreateUrlshortenerService()
        {
            var service = new UrlshortenerService(new BaseClientService.Initializer
            {
                ApiKey = ApiKey
            });

            return service;
        }

        #endregion
    }
}
