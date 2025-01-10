using url_shortener.Domain.Models;

namespace url_shortener.Domain.Interfaces.Repository;

public interface IUpdateable<T> where T : BaseEntity
{
    Task<T> UpdateAsync(T updatedEntity);
}