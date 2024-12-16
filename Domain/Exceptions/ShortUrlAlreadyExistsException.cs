namespace url_shortener.Domain.Exceptions;

public class ShortUrlAlreadyExistsException : Exception
{
    public string? ShortUrl { get; init; }
    
    public ShortUrlAlreadyExistsException(string message) : base(message) {}
    
    public ShortUrlAlreadyExistsException(string message, string shortUrl) 
        : base(message)
    {
        ShortUrl = shortUrl;
    }
}