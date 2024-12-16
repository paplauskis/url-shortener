using url_shortener.Data.Repositories;

namespace url_shortener.Services;

public class UrlAccessLogService
{
    private readonly UrlAccessLogRepository _repository;
    
    public UrlAccessLogService(UrlAccessLogRepository urlAccessLogRepository)
    {
        _repository = urlAccessLogRepository;
    }
}