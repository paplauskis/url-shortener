using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace url_shortener.Data.Models;

[Table("url_entity")]
public class UrlEntity : BaseEntity
{
    [Column("original_url")]
    [Required]
    [MinLength(4)]
    public string OriginalUrl { get; init; }
    
    [Column("shortened_url")]
    [MaxLength(50)]
    [Required]
    public string ShortenedUrl { get; init; }
    
    [Column("click_count")]
    public int ClickCount { get; set; }

    public UrlEntity(string originalUrl, string shortenedUrl, int clickCount = 0)
    {
        OriginalUrl = originalUrl;
        ShortenedUrl = shortenedUrl;
        ClickCount = clickCount;
    }
    
    public virtual List<UrlAccessLog> UrlAccessLogs { get; set; } = new List<UrlAccessLog>();
}