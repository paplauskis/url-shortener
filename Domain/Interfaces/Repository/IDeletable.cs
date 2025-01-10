using url_shortener.Domain.Models;

namespace url_shortener.Domain.Interfaces.Repository;

public interface IDeletable<T> where T : BaseEntity
{
    Task DeleteAsync(T entity);
}