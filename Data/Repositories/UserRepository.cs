using Microsoft.EntityFrameworkCore;
using url_shortener.Data.Context;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Interfaces.Repository;
using url_shortener.Domain.Models;

namespace url_shortener.Data.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        var entity = await _context.Users.FindAsync(id);

        if (entity != null)
        {
            return entity;
        }
        
        throw new EntityNotFoundException("Entity could not be found", id);
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        var entityToBeUpdated = await _context.Users.FindAsync(entity.Id);

        if (entityToBeUpdated != null)
        {
            _context.Entry(entityToBeUpdated).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }
        
        throw new EntityNotFoundException("Url entity not found", entity.Id);
    }

    public async Task DeleteAsync(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    
}