using System.Text.RegularExpressions;

namespace url_shortener.Helpers;

public static class UserCredentialValidator
{
    public static bool IsEmailValid(this string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }
        
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        
        return Regex.IsMatch(email, pattern);
    }

    public static bool IsPasswordValid(this string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return false;
        }

        string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,}$";
        
        return Regex.IsMatch(password, pattern);
    }
}