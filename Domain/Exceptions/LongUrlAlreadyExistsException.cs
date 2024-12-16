namespace url_shortener.Domain.Exceptions;

public class LongUrlAlreadyExistsException : Exception
{
    public string? Url { get; init; }
    
    public LongUrlAlreadyExistsException(string message) : base(message) {}
    
    public LongUrlAlreadyExistsException(string message, string url) 
        : base(message)
    {
        Url = url;
    }
}