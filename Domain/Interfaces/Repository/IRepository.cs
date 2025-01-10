using url_shortener.Domain.Models;

namespace url_shortener.Domain.Interfaces.Repository;

public interface IRepository<T> : 
    IReadableById<T>,
    ICreatable<T>,
    IDeletable<T>,
    IUpdateable<T>
    where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T updatedEntity);
    Task DeleteAsync(T entity);
}