namespace url_shortener.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public int EntityId { get; init; }
    
    public EntityNotFoundException(string message) : base(message) {}
    
    public EntityNotFoundException(string message, int entityId) 
        : base(message)
    {
        EntityId = entityId;
    }
}