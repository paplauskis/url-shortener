using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using url_shortener.Data.Repositories;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Models;
using url_shortener.Helpers;

namespace url_shortener.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly JwtService _jwtService;

    public UserService(UserRepository userRepository, JwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            throw new EntityNotFoundException("user could not be found: " + email);
        }

        return user;
    }

    public async Task<LoginUserResponseDto> HandleUserLogin(LoginUserRequestDto userRequestDto)
    {
        var result = await _jwtService.Authenticate(userRequestDto);

        if (result == null)
        {
            throw new UnauthorizedAccessException();
        }

        return result;
    }

    public void ValidateUserInput(LoginUserRequestDto userRequestDto)
    {
        if (!userRequestDto.Email.IsEmailValid())
        {
            throw new ArgumentException("Invalid email", userRequestDto.Email);
        }

        if (!userRequestDto.Password.IsPasswordValid())
        {
            throw new ArgumentException("Invalid password", userRequestDto.Password);
        }
    }

    public async Task<User> AddAsync(User user)
    {
        return await _userRepository.AddAsync(user);
    }
}