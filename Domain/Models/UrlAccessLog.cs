using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace url_shortener.Domain.Models;

[Table("url_access_log")]
public class UrlAccessLog : BaseEntity
{
    [Column("url_entity_id")]
    [Required]
    public int UrlEntityId { get; init; }
    
    [Column("accessed_at")]
    [Required]
    public DateTime AccessedAt { get; init; }
    
    [Column("ip_address")]
    [MaxLength(50)]
    [Required]
    public string IpAddress { get; init; }
    
    [Column("user_agent")]
    [MaxLength(300)]
    [Required]
    public string UserAgent { get; init; }
    
    public virtual UrlEntity UrlEntity { get; init; }

    public UrlAccessLog(int urlEntityId, DateTime accessedAt, string ipAddress, string userAgent)
    {
        UrlEntityId = urlEntityId;
        AccessedAt = accessedAt;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
}