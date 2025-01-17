using url_shortener.Data.Repositories;
using UAParser;
using url_shortener.Domain.Interfaces.Service;
using url_shortener.Domain.Models;

namespace url_shortener.Services;

public class UrlAccessLogService : IUrlAccessLogService
{
    private readonly UrlAccessLogRepository _repository;
    private string _lastRequestIp;
    private DateTime _lastRequestDate;
    
    public UrlAccessLogService(UrlAccessLogRepository urlAccessLogRepository)
    {
        _repository = urlAccessLogRepository;
        _lastRequestIp = string.Empty;
    }

    public async Task SaveRequestInfo(string userAgent, string? ip, int urlId, DateTime accessDate)
    {
        var parser = Parser.GetDefault();
        var clientInfo = parser.Parse(userAgent);

        var entity = new UrlAccessLog(
            urlId,
            ip ?? string.Empty,
            clientInfo.Device.Family,
            clientInfo.OS.ToString(),
            clientInfo.UA.ToString()
        );
        
        if (!IsDuplicateRequest(ip, accessDate))
        {
            await _repository.AddAsync(entity);
        }
    }
    
    //prevents from saving duplicate results to db
    private bool IsDuplicateRequest(string? ip, DateTime accessDate)
    {
        string str = ip ?? string.Empty;
        
        if (_lastRequestIp == str && _lastRequestDate == accessDate)
        {
            return true;
        }
        
        _lastRequestIp = str;
        _lastRequestDate = accessDate;
        
        return false;
    }
}
