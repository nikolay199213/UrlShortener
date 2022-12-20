using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using UrlShortener.Services;
using UrlShortener.Validators;

namespace UrlShortenerWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly IUrlService _urlService;
        private readonly ILogger<ShortenerController> _logger;
        private readonly IUrlValidator _urlValidator;
        private readonly IBarCodeService _barCodeService;

        public ShortenerController(IUrlService urlService, ILogger<ShortenerController> logger, IUrlValidator urlValidator, IBarCodeService barCodeService)
        {
            _urlService = urlService;
            _logger = logger;
            _urlValidator = urlValidator;
            _barCodeService = barCodeService;
        }
        [HttpGet("{shrotGuid}")]
        public async Task<IActionResult> Get(string shrotGuid)
        {
            
            var targetUrl = await _urlService.GetFullUrl(shrotGuid);
            if (targetUrl == "")
                return NotFound();

            return Redirect(targetUrl);
        }
        [HttpPost]
        public async Task<IActionResult> Post(string targetUrl)
        {
            var decodingUrl = HttpUtility.UrlDecode(targetUrl);

            if (!_urlValidator.Validate(decodingUrl))
                return BadRequest($"Url {decodingUrl} is not valid");

            var shortGuid = await _urlService.Reduce(decodingUrl);

            var host = Request.Host;
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var shortUrl = $"https://{host}/{controllerName}/{shortGuid}";

            var pathBarCode = $"wwwroot/{shortGuid}.html";
            _barCodeService.SaveBarcode(shortUrl, pathBarCode);

            return Ok(shortUrl);
        }
    }
}
