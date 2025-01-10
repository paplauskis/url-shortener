using url_shortener.Domain.DTOs;
using url_shortener.Domain.Interfaces.Repository;

namespace url_shortener.Services;

public class UserValidationService
{
    private readonly IEnumerable<IValidator<LoginUserRequestDto>> _validators;

    public UserValidationService(IEnumerable<IValidator<LoginUserRequestDto>> validators)
    {
        _validators = validators;
    }

    public bool Validate(LoginUserRequestDto user)
    {
        
        foreach (var validator in _validators)
        {
            if (!validator.IsValid(user))
            {
                return false;
            }
        }
        
        return true;
    }
}