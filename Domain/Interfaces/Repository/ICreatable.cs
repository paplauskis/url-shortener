using url_shortener.Domain.Models;

namespace url_shortener.Domain.Interfaces.Repository;

public interface ICreatable<T> where T : BaseEntity
{
    Task<T> AddAsync(T entity);
}