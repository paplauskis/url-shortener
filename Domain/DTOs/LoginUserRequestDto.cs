namespace url_shortener.Domain.DTOs;

public class LoginUserRequestDto
{
    public string? Password { get; set; }
    public string? Email { get; set; }
}