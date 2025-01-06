using System.IdentityModel.Tokens.Jwt;
using url_shortener.Data.Repositories;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Models;
using url_shortener.Helpers;

namespace url_shortener.Services;

public class UrlEntityService
{
    private readonly UrlEntityRepository _urlEntityRepository;
    private readonly UserService _userService;

    public UrlEntityService(UrlEntityRepository urlEntityRepository, UserService userService)
    {
        _urlEntityRepository = urlEntityRepository;
        _userService = userService;
    }

    public async Task<List<UrlEntity>> GetAllUrlEntitiesAsync()
    {
        return await _urlEntityRepository.GetAllAsync();
    }

    public async Task<List<UrlEntity>> GetUrlEntitiesWithAccessLogsAsync()
    {
        try
        {
            return await _urlEntityRepository.GetAllWithAccessLogsAsync();
        }
        catch (ArgumentNullException ane)
        {
            throw new ArgumentNullException(ane.Message);
        }
    }
    
    public async Task<UrlEntity> GetUrlEntityWithAccessLogsByIdAsync(string id)
    {
        if (!int.TryParse(id, out int intId))
        {
            throw new InvalidOperationException("Id must be a number.");
        }
        
        try
        {
            var entity = await _urlEntityRepository.GetEntityWithAccessLogsByIdAsync(intId);
            
            if (entity != null)
            {
                return entity;    
            }
            
            throw new EntityNotFoundException("Url wasn't found.", intId);
        }
        catch (ArgumentNullException ane)
        {
            throw new ArgumentNullException(ane.Message);
        }
    }
    
    public async Task<UrlEntity?> GetUrlEntityByIdAsync(int id)
    {
        return await _urlEntityRepository.GetByIdAsync(id);
    }

    public async Task<UrlEntity> GetUrlEntityByShortUrlAsync(string shortUrl)
    {
        try
        {
            var entity = await _urlEntityRepository.GetByShortUrlAsync(shortUrl);
            return entity;
        }
        catch (EntityNotFoundException enfe)
        {
            throw new EntityNotFoundException(enfe.Message);
        }
    }
    
    public async Task<UrlEntity> CreateUrlEntityAsync(string url, string token)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url), "Url cannot be null or empty");
        }
        
        if (_urlEntityRepository.CheckIfLongUrlExists(url))
        {
            throw new LongUrlAlreadyExistsException("Url already exists", url);
        }

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var userEmailClaim = jsonToken?.Claims.FirstOrDefault(c => 
            string.Equals(c.Type, "email", StringComparison.OrdinalIgnoreCase))?.Value;
        var user = await _userService.GetByEmailAsync(userEmailClaim);
        
        int totalNumOfEntities = await _urlEntityRepository.CountEntitiesAsync();
        string shortUrl;
        
        do
        {
            shortUrl = ShortUrlGenerator.Generate(totalNumOfEntities);
        } while (_urlEntityRepository.CheckIfShortUrlExists(shortUrl));
        
        var entity = new UrlEntity(url, shortUrl, user.Id);
        
        return await _urlEntityRepository.AddAsync(entity);
    }

    public async Task<UrlEntity> UpdateUrlEntityAsync(string id, UrlEntityDto urlEntityDto)
    {
        if (!int.TryParse(id, out int intId))
        {
            throw new InvalidOperationException("Id must be a number.");
        }
        
        var entity = await GetUrlEntityByIdAsync(intId);

        if (entity == null)
        {
            throw new EntityNotFoundException("Url entity not found", intId);
        }

        if (urlEntityDto.ShortenedUrl != entity.ShortenedUrl && _urlEntityRepository.CheckIfShortUrlExists(urlEntityDto.ShortenedUrl))
        {
            throw new ShortUrlAlreadyExistsException("Url already exists", urlEntityDto.ShortenedUrl);
        }
        
        entity.UpdatedAt = DateTime.UtcNow;
        
        return await _urlEntityRepository.UpdateAsync(entity, urlEntityDto);
    }

    public async Task DeleteUrlEntityAsync(string id)
    {
        if (!int.TryParse(id, out int intId))
        {
            throw new InvalidOperationException("Id must be a number.");
        }
        
        var entity = await _urlEntityRepository.GetByIdAsync(intId);

        await _urlEntityRepository.DeleteAsync(entity);
    }

    public async Task IncrementClickCount(UrlEntity urlEntity)
    {
        urlEntity.ClickCount++;
        await _urlEntityRepository.UpdateAsync(urlEntity);
    }
}