using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using url_shortener.Data.Context;
using url_shortener.Domain.DTOs;
using url_shortener.Domain.Exceptions;
using url_shortener.Services;

namespace url_shortener.Controllers;

[ApiController]
[AllowAnonymous]
[Route("/api/[controller]/")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequestDto userRequestDto)
    {
        try
        {
            var response = await _userService.HandleUserLogin(userRequestDto);
            return Ok(response);
        }
        catch (UnauthorizedAccessException uae)
        {
            return Unauthorized("unauthorized access");
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginUserRequestDto userRequestDto)
    {
        try
        {
            var response = await _userService.HandleUserRegistration(userRequestDto);
            return Ok(response);
        }
        catch (UserAlreadyExistsException e)
        {
            return BadRequest(e.Message + ": " + e.Email);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message + ": " + e.ParamName);
        }
    }
}