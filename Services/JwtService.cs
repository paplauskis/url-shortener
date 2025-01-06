using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using url_shortener.Data.Repositories;
using url_shortener.Domain.DTOs;

namespace url_shortener.Services;

public class JwtService
{
    private readonly UserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public JwtService(UserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<LoginUserResponseDto?> Authenticate(LoginUserRequestDto requestDto)
    {
        var userAccount = await _userRepository.GetByEmailAsync(requestDto.Email);
        if (userAccount is null)
        {
            return null;
        }

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:Key"];
        var tokenValidityMins = _configuration.GetValue<int>("Jwt:TokenValidityMins");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, requestDto.Email),
            }),
            Expires = tokenExpiryTimeStamp,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new LoginUserResponseDto
        {
            AccessToken = accessToken,
            Email = requestDto.Email,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
        };
    }
}