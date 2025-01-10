using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Interfaces.Service;
using url_shortener.Domain.Models;
using url_shortener.Services;

namespace url_shortener.Controllers;

[ApiController]
[Authorize]
[Route("/api/")]
public class UrlController : ControllerBase
{
    private readonly IUrlEntityService _urlEntityService;
    private readonly IUrlAccessLogService _urlAccessLogService;

    public UrlController(IUrlEntityService urlEntityService, IUrlAccessLogService urlAccessLog)
    {
        _urlEntityService = urlEntityService;
        _urlAccessLogService = urlAccessLog;
    }

    [HttpGet]
    public async Task<ActionResult<List<UrlEntity>>> GetUrls()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var entities = await _urlEntityService.GetAllAsync(token);
        return Ok(entities);
    }

    [HttpGet("url/{id}")]
    public async Task<ActionResult<UrlEntity>> GetUrlDataById([FromRoute] string id)
    {
        try
        {
            var entity = await _urlEntityService.GetUrlEntityWithAccessLogsByIdAsync(id);
            return Ok(entity);
        }
        catch (Exception e) 
            when (e is EntityNotFoundException or ArgumentNullException)
        {
            return NotFound(e.Message);
        }
        catch (InvalidOperationException ioe)
        {
            return BadRequest(ioe.Message);
        }
    }

    [HttpGet("{shortUrl}")]
    [AllowAnonymous]
    public async Task<ActionResult<UrlEntity>> RedirectUrl(string shortUrl)
    {
        try
        {
            var entity = await _urlEntityService.GetUrlEntityByShortUrlAsync(shortUrl);
            var request = HttpContext.Request.Headers["User-Agent"].ToString();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            await _urlAccessLogService.SaveRequestInfo(request, ip, entity.Id, entity.CreatedAt);
            
            await _urlEntityService.IncrementClickCount(entity);
            
            return Redirect(entity.OriginalUrl);
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
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            
            var newUrl = await _urlEntityService.CreateUrlEntityAsync(url, token);
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

    [HttpDelete("url/{id}")]
    public async Task<IActionResult> DeleteUrlEntity([FromRoute] string id)
    {
        try
        {
            await _urlEntityService.DeleteUrlEntityAsync(id);
            return Ok();
        }
        catch (InvalidOperationException ioe)
        {
            return BadRequest(ioe.Message);
        }
    }

    [HttpGet("stats")]
    public async Task<ActionResult<List<UrlEntity>>> GetUrlEntitiesWithAccessLogs()
    {
        try
        {
            var logs = await _urlEntityService.GetAllWithAccessLogsAsync();
            return Ok(logs);
        }
        catch (ArgumentNullException ane)
        {
            return NotFound(ane.Message);
        }
    }
}