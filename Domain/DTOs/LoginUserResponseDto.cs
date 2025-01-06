namespace url_shortener.Domain.DTOs;

public class LoginUserResponseDto
{
    public string? Email { get; set; }
    public string? AccessToken { get; set; }
    public int ExpiresIn { get; set; }
}