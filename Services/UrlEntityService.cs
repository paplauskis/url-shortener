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
    
    public async Task<UrlEntity> CreateUrlEntity(string url)
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
}