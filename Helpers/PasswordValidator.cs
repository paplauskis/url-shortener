using System.Text.RegularExpressions;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Interfaces.Repository;

namespace url_shortener.Helpers;

public class PasswordValidator : IValidator<LoginUserRequestDto>
{
    public bool IsValid(LoginUserRequestDto user)
    {
        if (string.IsNullOrEmpty(user.Password))
        {
            return false;
        }

        string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,}$";
        
        return Regex.IsMatch(user.Password, pattern);
    }
}