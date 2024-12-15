namespace url_shortener.Domain;

public class UrlAlreadyExistsException : Exception
{
    public string Url { get; init; }
    
    public UrlAlreadyExistsException(string message) : base(message) {}
    
    public UrlAlreadyExistsException(string message, string url) 
        : base(message)
    {
        Url = url;
    }
}