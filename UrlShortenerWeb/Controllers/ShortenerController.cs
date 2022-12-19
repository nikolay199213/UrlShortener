using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using UrlShortener.Services;

namespace UrlShortenerWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly IUrlService _urlService;
        private readonly ILogger<ShortenerController> _logger;

        public ShortenerController(IUrlService urlService, ILogger<ShortenerController> logger)
        {
            _urlService = urlService;
            _logger = logger;
        }
        [HttpGet("{shrotGuid}")]
        public async Task<IActionResult> Get(string shrotGuid)
        {
            
            var targetUrl = await _urlService.GetFullUrl(shrotGuid);
            if (targetUrl == null)
                return NotFound();
            else
            {
                return Redirect(targetUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(string targetUrl)
        {
            var decodingUrl = HttpUtility.UrlDecode(targetUrl);
            var shortGuid = await _urlService.Reduce(decodingUrl);
            var host = Request.Host;
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            return Ok($"https://{host}/{controllerName}/{shortGuid}");
        }
    }
}
