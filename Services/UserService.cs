using System.IdentityModel.Tokens.Jwt;
using url_shortener.Data.Repositories;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Exceptions;
using url_shortener.Domain.Interfaces.Repository;
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

    public async Task<LoginUserResponseDto> HandleUserRegistration(LoginUserRequestDto userRequestDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(userRequestDto.Email);

        if (existingUser != null)
        {
            throw new UserAlreadyExistsException("User with this email already exists", existingUser.Email);
        }
        
        var validators = new List<IValidator<LoginUserRequestDto>> { new EmailValidator(), new PasswordValidator() };
        var validationService = new UserValidationService(validators);
        
        if (!validationService.Validate(userRequestDto))
        {
            throw new ArgumentException("Email or password is invalid");
        }
        
        await AddAsync(new User { Email = userRequestDto.Email, Password = userRequestDto.Password });

        return await HandleUserLogin(userRequestDto);
    }

    public async Task<User> AddAsync(User user)
    {
        return await _userRepository.AddAsync(user);
    }

    public async Task<User> ValidateUserAsync(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var userEmailClaim = jsonToken?.Claims.FirstOrDefault(c => 
            string.Equals(c.Type, "email", StringComparison.OrdinalIgnoreCase))?.Value;

        if (userEmailClaim is null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var user = await GetByEmailAsync(userEmailClaim);
        
        return user;
    }
}