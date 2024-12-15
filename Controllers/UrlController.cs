using Microsoft.AspNetCore.Mvc;
using url_shortener.Domain;
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
        try
        {
            var newUrl = await _urlEntityService.CreateUrlEntity(url);

            return Ok(newUrl);
        }
        catch (UrlAlreadyExistsException uae)
        {
            return Conflict(uae.Message);
        }
        catch (ArgumentNullException ane)
        {
            return BadRequest(ane.Message);
        }
        
    }
    
}