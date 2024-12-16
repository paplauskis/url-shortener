using url_shortener.Data.Repositories;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Models;
using url_shortener.Helpers;

namespace url_shortener.Services;

public class UrlEntityService
{
    private readonly UrlEntityRepository _repository;

    public UrlEntityService(UrlEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UrlEntity>> GetAllUrlEntitiesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<UrlEntity> GetUrlEntityByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<UrlEntity> GetUrlEntityByShortUrlAsync(string shortUrl)
    {
        try
        {
            var entity = await _repository.GetByShortUrlAsync(shortUrl);
            return entity;
        }
        catch (EntityNotFoundException enfe)
        {
            throw new EntityNotFoundException(enfe.Message);
        }
    }
    
    public async Task<UrlEntity> CreateUrlEntityAsync(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url), "Url cannot be null or empty");
        }
        
        if (_repository.CheckIfLongUrlExists(url))
        {
            throw new UrlAlreadyExistsException("Url already exists", url);
        }
        
        int totalNumOfEntities = await _repository.CountEntitiesAsync();
        string shortUrl;
        
        do
        {
            shortUrl = ShortUrlGenerator.Generate(totalNumOfEntities);
        } while (_repository.CheckIfShortUrlExists(shortUrl));
        
        var entity = new UrlEntity(url, shortUrl);
        
        return await _repository.AddAsync(entity);
    }

    public async Task<UrlEntity> UpdateUrlEntityAsync(string id)
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
        
        entity.UpdatedAt = DateTime.UtcNow;
        
        return await _repository.UpdateAsync(entity);
    }
}