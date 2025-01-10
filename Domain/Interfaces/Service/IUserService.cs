using url_shortener.Domain.DTOs;
using url_shortener.Domain.Models;

namespace url_shortener.Domain.Interfaces.Service;

public interface IUserService
{
    Task<User> GetByEmailAsync(string email);
    Task<LoginUserResponseDto> HandleUserLogin(LoginUserRequestDto userRequestDto);
    Task<LoginUserResponseDto> HandleUserRegistration(LoginUserRequestDto userRequestDto);
    Task<User> AddAsync(User user);
    Task<User> ValidateUserTokenAsync(string token);
}