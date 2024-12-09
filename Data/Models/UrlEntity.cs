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

    public UrlEntity(string originalUrl, string shortenedUrl)
    {
        OriginalUrl = originalUrl;
        ShortenedUrl = shortenedUrl;
    }
    
    public virtual List<UrlAccessLog> UrlAccessLogs { get; set; } = new List<UrlAccessLog>();
}