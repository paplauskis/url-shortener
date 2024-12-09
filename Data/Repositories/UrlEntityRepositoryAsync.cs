using Microsoft.EntityFrameworkCore;
using url_shortener.Data.Context;
using url_shortener.Data.Models;
using url_shortener.Domain;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Interfaces.Repository;

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
        return await _context.UrlEntities.ToListAsync();
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

    public async Task<UrlEntity> AddAsync(UrlEntity entity)
    {
        await _context.UrlEntities.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<UrlEntity> UpdateAsync(UrlEntity updatedEntity)
    {
        var existingEntity = await _context.UrlEntities.FindAsync(updatedEntity.Id);

        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
            
            return updatedEntity;
        }
        
        throw new EntityNotFoundException("Url entity not found", updatedEntity.Id);
    }

    public async Task DeleteAsync(int id)
    {
        var existingEntity = await _context.UrlEntities.FindAsync(id);
        
        if (existingEntity != null)
        {
            _context.Remove(existingEntity);
            await _context.SaveChangesAsync();
        }
    }
}