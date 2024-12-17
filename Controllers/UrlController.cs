using Microsoft.AspNetCore.Mvc;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Models;
using url_shortener.Services;

namespace url_shortener.Controllers;

[ApiController]
[Route("/api/")]
public class UrlController : ControllerBase
{
    private readonly UrlEntityService _urlEntityService;
    private readonly UrlAccessLogService _urlAccessLogService;

    public UrlController(UrlEntityService urlEntityService, UrlAccessLogService urlAccessLog)
    {
        _urlEntityService = urlEntityService;
        _urlAccessLogService = urlAccessLog;
    }

    [HttpGet]
    public async Task<ActionResult<List<UrlEntity>>> GetUrls()
    {
        var entities = await _urlEntityService.GetAllUrlEntitiesAsync();
        return Ok(entities);
    }

    [HttpGet("{shortUrl}")]
    public async Task<ActionResult<UrlEntity>> RedirectUrl(string shortUrl)
    {
        try
        {
            var entity = await _urlEntityService.GetUrlEntityByShortUrlAsync(shortUrl);
            var request = HttpContext.Request.Headers["User-Agent"].ToString();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _urlAccessLogService.SaveRequestInfo(request, ip, entity.Id, entity.CreatedAt);
            await _urlEntityService.IncrementClickCount(entity);
            
            return Redirect("https://" + entity.OriginalUrl);
        }
        catch (EntityNotFoundException enfe)
        {
            return NotFound(enfe.Message);
        }
    }
    
    [HttpPost("shorten/{url}")]
    public async Task<IActionResult> CreateNewUrl([FromRoute] string url)
    {
        try
        {
            var newUrl = await _urlEntityService.CreateUrlEntityAsync(url);
            return Ok(newUrl);
        }
        catch (LongUrlAlreadyExistsException uae)
        {
            return Conflict(uae.Message);
        }
        catch (ArgumentNullException ane)
        {
            return BadRequest(ane.Message);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUrlEntity([FromRoute] string id, [FromBody] UrlEntityDto urlEntityDto)
    {
        try
        {
            var entity = await _urlEntityService.UpdateUrlEntityAsync(id, urlEntityDto);
            return Ok(entity);
        }
        catch (EntityNotFoundException enfe)
        {
            return NotFound(enfe.Message);
        }
        catch (InvalidOperationException ioe)
        {
            return BadRequest(ioe.Message);
        }
        catch (ShortUrlAlreadyExistsException suae)
        {
            return Conflict(suae.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUrlEntity([FromRoute] string id)
    {
       await _urlEntityService.DeleteUrlEntityAsync(id);
       return Ok();
    }

    [HttpGet("stats")]
    public async Task<ActionResult<List<UrlEntity>>> GetUrlAccessLogs()
    {
        try
        {
            var logs = await _urlAccessLogService.GetUrlAccessLogs();
            return Ok(logs);
        }
        catch (ArgumentNullException ane)
        {
            return NotFound(ane.Message);
        }
    }
}