using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace url_shortener.Domain.Models;

[Table("url_access_log")]
public class UrlAccessLog : BaseEntity
{
    [Column("url_entity_id")]
    [Required]
    public int UrlEntityId { get; init; }
    
    [Column("ip_address")]
    [MaxLength(50)]
    [Required]
    public string? IpAddress { get; init; }
    
    [Column("device_type")]
    [MaxLength(20)]
    [Required]
    public string Device { get; init; }
    
    [Column("operating_system")]
    [MaxLength(50)]
    [Required]
    public string OperatingSystem { get; init; }
    
    [Column("browser")]
    [MaxLength(50)]
    [Required]
    public string Browser { get; init; }
    
    [JsonIgnore]
    public virtual UrlEntity UrlEntity { get; init; }

    public UrlAccessLog(int urlEntityId, string ipAddress, string device, string operatingSystem, string browser)
    {
        UrlEntityId = urlEntityId;
        IpAddress = ipAddress;
        Device = device;
        OperatingSystem = operatingSystem;
        Browser = browser;
    }
}