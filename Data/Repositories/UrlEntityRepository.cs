using Microsoft.EntityFrameworkCore;
using url_shortener.Data.Context;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Interfaces.Repository;
using url_shortener.Domain.Models;

namespace url_shortener.Data.Repositories;

public class UrlEntityRepository : IRepository<UrlEntity>
{
    private readonly AppDbContext _context;
    
    public UrlEntityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UrlEntity>> GetAllAsync()
    {
        return await _context.UrlEntities.OrderByDescending(e => e.CreatedAt).ToListAsync();
    }
    
    public async Task<List<UrlEntity>> GetAllWithAccessLogsAsync()
    {
        try
        {
            return await _context.UrlEntities
                .Include(e => e.UrlAccessLogs)
                .ToListAsync();
        }
        catch (ArgumentNullException ane)
        {
            throw new ArgumentException(ane.Message);
        }
    }
    
    public async Task<UrlEntity> GetEntityWithAccessLogsByIdAsync(int id)
    {
        try
        {
            var entity = await _context.UrlEntities
                .Where(e => e.Id == id)
                .Include(e => e.UrlAccessLogs)
                .FirstOrDefaultAsync();
            
            if (entity != null)
            {
                return entity;    
            }
            
            throw new EntityNotFoundException("Url wasn't found.", id);
        }
        catch (ArgumentNullException ane)
        {
            throw new ArgumentException(ane.Message);
        }
    }
    
    public async Task<int> CountEntitiesAsync()
    {
        return await _context.UrlEntities.CountAsync();
    }

    public async Task<UrlEntity> GetByIdAsync(int id)
    {
        var entity = await _context.UrlEntities.FindAsync(id);

        if (entity != null)
        {
            return entity;
        }
        
        throw new EntityNotFoundException("Entity could not be found", id);
    }
    
    public async Task<UrlEntity> GetByShortUrlAsync(string url)
    {
        var entity = await _context.UrlEntities.FirstOrDefaultAsync(e => e.ShortenedUrl == url);

        if (entity != null)
        {
            return entity;
        }
        
        throw new EntityNotFoundException("Entity could not be found");
    }

    public async Task<UrlEntity> AddAsync(UrlEntity entity)
    {
        await _context.UrlEntities.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<UrlEntity> UpdateAsync(UrlEntity entityToBeUpdated, UrlEntityDto dto)
    {
            _context.Entry(entityToBeUpdated).CurrentValues.SetValues(dto);
            await _context.SaveChangesAsync();
            
            return entityToBeUpdated;
    }
    
    public async Task<UrlEntity> UpdateAsync(UrlEntity entity)
    {
        var entityToBeUpdated = await _context.UrlEntities.FindAsync(entity.Id);

        if (entityToBeUpdated != null)
        {
            _context.Entry(entityToBeUpdated).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }
        
        throw new EntityNotFoundException("Url entity not found", entity.Id);
    }

    public async Task DeleteAsync(UrlEntity entity)
    {
            _context.UrlEntities.Remove(entity);
            await _context.SaveChangesAsync();
    }
    
    public bool CheckIfShortUrlExists(string url)
    {
        return _context.UrlEntities
            .Any(e => e.ShortenedUrl == url);
    }
    
    public bool CheckIfLongUrlExists(string url)
    {
        return _context.UrlEntities
            .Any(e => e.OriginalUrl == url);
    }
}