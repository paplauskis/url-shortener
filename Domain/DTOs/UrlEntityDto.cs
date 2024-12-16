namespace url_shortener.Domain.DTOs;

public class UrlEntityDto
{
    public string OriginalUrl { get; init; }
    public string ShortenedUrl { get; init; }

    public UrlEntityDto(string originalUrl, string shortenedUrl)
    {
        OriginalUrl = originalUrl;
        ShortenedUrl = shortenedUrl;
    }
}