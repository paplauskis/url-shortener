using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace url_shortener.Domain.Models;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; private set; }

    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
}