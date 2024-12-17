using url_shortener.Data.Repositories;
using UAParser;
using url_shortener.Domain.Models;

namespace url_shortener.Services;

public class UrlAccessLogService
{
    private readonly UrlAccessLogRepository _repository;
    
    public UrlAccessLogService(UrlAccessLogRepository urlAccessLogRepository)
    {
        _repository = urlAccessLogRepository;
    }

    public async Task SaveRequestInfo(string userAgent, string ip, int urlId, DateTime accessDate)
    {
        var parser = Parser.GetDefault();
        var clientInfo = parser.Parse(userAgent);

        var entity = new UrlAccessLog(
            urlId,
            ip,
            clientInfo.Device.Family,
            clientInfo.OS.ToString(),
            clientInfo.UA.ToString()
        );
        
        await _repository.AddAsync(entity);
    }
}