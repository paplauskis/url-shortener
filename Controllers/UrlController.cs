using Microsoft.AspNetCore.Mvc;
using url_shortener.Services;

namespace url_shortener.Controllers;

[ApiController]
[Route("/api")]
public class UrlController : ControllerBase
{
    private readonly UrlEntityService _urlEntityService;

    public UrlController(UrlEntityService urlEntityService)
    {
        _urlEntityService = urlEntityService;
    }
    
    [HttpPost("/shorten/{url}")]
    public async Task<IActionResult> CreateNewUrl([FromRoute] string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return BadRequest("URL is required");
        }

        var newUrl = await _urlEntityService.CreateUrlEntity(url);
        
        return Ok(newUrl);
    }
    
}