using url_shortener.Data.Models;

namespace url_shortener.Domain.Interfaces.Repository;

public interface IRepositoryAsync<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T updatedEntity);
    Task DeleteAsync(int id);
}