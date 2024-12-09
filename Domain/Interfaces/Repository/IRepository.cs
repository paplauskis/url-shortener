using url_shortener.Data.Models;

namespace url_shortener.Domain.Interfaces.Repository;

public interface IRepository<T> where T : BaseEntity
{
    List<T> GetAllAsync();
    T GetByIdAsync(int id);
    T AddAsync(T entity);
    T UpdateAsync(T updatedEntity);
    T DeleteAsync(int id);
}