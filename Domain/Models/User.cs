using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace url_shortener.Domain.Models;

[Table("user")]
public class User : BaseEntity
{
    [Column("email")]
    [Required]
    public string Email { get; set; }
    
    [Column("password")]
    [Required]
    public string Password { get; set; }
    
    public virtual List<UrlEntity> Urls { get; set; } = new List<UrlEntity>();
}