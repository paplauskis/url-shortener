using Microsoft.EntityFrameworkCore;
using url_shortener.Data.Models;

namespace url_shortener.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<UrlEntity> UrlEntities { get; set; }
    public DbSet<UrlAccessLog> UrlAccessLogs { get; set; }
}