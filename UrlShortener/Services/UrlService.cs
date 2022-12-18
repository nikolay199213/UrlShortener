using Microsoft.EntityFrameworkCore;
using shortid;
using shortid.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly UrlShortenerContext _urlShortenerContext;
        private GenerationOptions generationOptions => new GenerationOptions(true, false, 8);

        public UrlService(UrlShortenerContext urlShortenerContext)
        {
            _urlShortenerContext = urlShortenerContext;
        }
        public async Task<string> GetFullUrl(string shortGuid)
        {
            var url = await _urlShortenerContext.Urls.FirstOrDefaultAsync(x => x.ShortGuid == shortGuid);

            return url.TargetUrl;
        }
        public async Task<string> Reduce(string targetUrl)
        {
            var shortGuid = ShortId.Generate(generationOptions);
            var newUrl = new Url { TargetUrl = targetUrl, ShortGuid = shortGuid };
            await _urlShortenerContext.Urls.AddAsync(newUrl);
            await _urlShortenerContext.SaveChangesAsync();
            return shortGuid;
        }
    }
}
