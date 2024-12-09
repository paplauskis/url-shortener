namespace url_shortener.Domain.DTOs;

public class UrlEntityDto
{
    public string OriginalUrl { get; init; }
    public string ShortenedUrl { get; init; }
    public int ClickCount { get; set; }

    public UrlEntityDto(string originalUrl, string shortenedUrl, int clickCount = 0)
    {
        OriginalUrl = originalUrl;
        ShortenedUrl = shortenedUrl;
        ClickCount = clickCount;
    }
}