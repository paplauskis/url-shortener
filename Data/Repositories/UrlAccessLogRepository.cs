using Microsoft.EntityFrameworkCore;
using url_shortener.Data.Context;
using url_shortener.Data.Models;
using url_shortener.Domain;
using url_shortener.Domain.Interfaces.Repository;

namespace url_shortener.Data.Repositories;

public class UrlAccessLogRepository : IRepository<UrlAccessLog>
{
    private readonly AppDbContext _context;
    
    public UrlAccessLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UrlAccessLog>> GetAllAsync()
    {
        return await _context.UrlAccessLogs.ToListAsync();
    }

    public async Task<UrlAccessLog> GetByIdAsync(int id)
    {
        var entity = await _context.UrlAccessLogs.FindAsync(id);

        if (entity != null)
        {
            return entity;
        }
        
        throw new EntityNotFoundException("Entity could not be found", id);
    }

    public async Task<UrlAccessLog> AddAsync(UrlAccessLog entity)
    {
        await _context.UrlAccessLogs.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<UrlAccessLog> UpdateAsync(UrlAccessLog updatedEntity)
    {
        var existingEntity = await _context.UrlAccessLogs.FindAsync(updatedEntity.Id);

        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            
            return updatedEntity;
        }
        
        throw new EntityNotFoundException("Url log entity not found", updatedEntity.Id);
    }

    public async Task DeleteAsync(int id)
    {
        var existingEntity = await _context.UrlAccessLogs.FindAsync(id);
        
        if (existingEntity != null)
        {
            _context.UrlAccessLogs.Remove(existingEntity);
            await _context.SaveChangesAsync();
        }
    }
}