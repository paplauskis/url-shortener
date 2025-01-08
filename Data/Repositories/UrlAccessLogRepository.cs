using Microsoft.EntityFrameworkCore;
using url_shortener.Data.Context;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Interfaces.Repository;
using url_shortener.Domain.Models;

namespace url_shortener.Data.Repositories;

public class UrlAccessLogRepository : BaseRepository<UrlAccessLog>
{
    public UrlAccessLogRepository(AppDbContext context) : base(context) {}

    public async Task<List<UrlAccessLog>> GetAllAsync()
    {
        try
        {
            return await Context.UrlAccessLogs.ToListAsync();
        }
        catch (ArgumentNullException ane)
        {
            throw new ArgumentNullException(ane.Message);
        }
    }
}