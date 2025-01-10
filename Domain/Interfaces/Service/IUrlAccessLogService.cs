namespace url_shortener.Domain.Interfaces.Service;

public interface IUrlAccessLogService
{
    Task SaveRequestInfo(string userAgent, string? ip, int urlId, DateTime accessDate);
}