namespace url_shortener.Domain.Interfaces.Repository;

public interface IValidator<T>
{
    bool IsValid(T entity);
}