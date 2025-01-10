using url_shortener.Domain.Models;

namespace url_shortener.Domain.Interfaces.Repository;

public interface IReadableById<T>  where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
}