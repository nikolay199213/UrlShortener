using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UrlService> _logger;
        private GenerationOptions generationOptions => new GenerationOptions(true, false, 8);

        public UrlService(UrlShortenerContext urlShortenerContext)
        {
            _urlShortenerContext = urlShortenerContext;
        }
        public async Task<string> GetFullUrl(string shortGuid)
        {
            var url = await _urlShortenerContext.Urls.FirstOrDefaultAsync(x => x.ShortGuid == shortGuid);
            if (url == null)
                return "";            
            return url.TargetUrl;
        }
        public async Task<string> Reduce(string targetUrl)
        {
            var shortGuid = ShortId.Generate(generationOptions);
            var newUrl = new Url { TargetUrl = targetUrl, ShortGuid = shortGuid };
            await SaveUrl(newUrl);
            return shortGuid;
        }
        public async Task SaveUrl(Url url)
        {
            await _urlShortenerContext.Urls.AddAsync(url);
            await _urlShortenerContext.SaveChangesAsync();

        }
    }
}
