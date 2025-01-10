using System.Text.RegularExpressions;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Interfaces.Repository;

namespace url_shortener.Helpers;

public class EmailValidator : IValidator<LoginUserRequestDto>
{
    public bool IsValid(LoginUserRequestDto user)
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            return false;
        }
        
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        
        return Regex.IsMatch(user.Email, pattern);
    }
}