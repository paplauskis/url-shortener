using Microsoft.EntityFrameworkCore;
using url_shortener.Data.Context;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Interfaces.Repository;
using url_shortener.Domain.Models;

namespace url_shortener.Data.Repositories;

public class UrlEntityRepository : BaseRepository<UrlEntity>
{
    public UrlEntityRepository(AppDbContext context) : base(context) {}

    public async Task<List<UrlEntity>> GetAllAsync(int userId)
    {
        return await Context.UrlEntities
            .Where(x => x.UserId == userId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<List<UrlEntity>> GetAllWithAccessLogsAsync()
    {
        try
        {
            return await Context.UrlEntities
                .Include(e => e.UrlAccessLogs)
                .ToListAsync();
        }
        catch (ArgumentNullException ane)
        {
            throw new ArgumentException(ane.Message);
        }
    }
    
    public async Task<UrlEntity?> GetEntityWithAccessLogsByIdAsync(int id)
    {
        try
        {
            return await Context.UrlEntities
                .Where(e => e.Id == id)
                .Include(e => e.UrlAccessLogs)
                .FirstOrDefaultAsync();
        }
        catch (ArgumentNullException ane)
        {
            throw new ArgumentException(ane.Message);
        }
    }
    
    public async Task<int> CountEntitiesAsync()
    {
        return await Context.UrlEntities.CountAsync();
    }
    
    public async Task<UrlEntity> GetByShortUrlAsync(string url)
    {
        var entity = await Context.UrlEntities.FirstOrDefaultAsync(e => e.ShortenedUrl == url);

        if (entity != null)
        {
            return entity;
        }
        
        throw new EntityNotFoundException("Entity could not be found");
    }

    public async Task<UrlEntity> UpdateAsync(UrlEntity entityToBeUpdated, UrlEntityDto dto)
    {
            Context.Entry(entityToBeUpdated).CurrentValues.SetValues(dto);
            await Context.SaveChangesAsync();
            
            return entityToBeUpdated;
    }
    
    public bool CheckIfShortUrlExists(string url)
    {
        return Context.UrlEntities
            .Any(e => e.ShortenedUrl == url);
    }
    
    public bool CheckIfLongUrlExists(string url)
    {
        return Context.UrlEntities
            .Any(e => e.OriginalUrl == url);
    }
}