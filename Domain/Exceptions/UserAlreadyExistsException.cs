namespace url_shortener.Domain.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public string Email { get; init; }
    public int Id { get; init; }
    
    public UserAlreadyExistsException(string message) : base(message) {}

    public UserAlreadyExistsException(string message, string email)
        : base(message)
    {
        Email = email;
    }

    public UserAlreadyExistsException(string message, int id)
        : base(message)
    {
        Id = id;
    }
}