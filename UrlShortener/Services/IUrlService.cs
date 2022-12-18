namespace UrlShortener.Services
{
    public interface IUrlService
    {
        Task<string> GetFullUrl(string shortGuid);
        Task<string> Reduce(string targetUrl);
    }
}