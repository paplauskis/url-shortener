using url_shortener.Domain.DTOs;
using url_shortener.Domain.Models;

namespace url_shortener.Domain.Interfaces.Service;

public interface IUrlEntityService
{
    Task<List<UrlEntity>> GetAllAsync(string token);
    Task<UrlEntity> GetUrlEntityWithAccessLogsByIdAsync(string id);
    Task<UrlEntity> GetUrlEntityByShortUrlAsync(string shortUrl);
    Task IncrementClickCount(UrlEntity urlEntity);
    Task<UrlEntity> CreateUrlEntityAsync(string url, string token);
    Task<UrlEntity> UpdateUrlEntityAsync(string id, UrlEntityDto urlEntityDto);
    Task DeleteUrlEntityAsync(string id);
    Task<List<UrlEntity>> GetAllWithAccessLogsAsync();
}