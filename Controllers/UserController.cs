using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using url_shortener.Data.Context;
using url_shortener.Domain.DTOs;
using url_shortener.Services;

namespace url_shortener.Controllers;

[ApiController]
[AllowAnonymous]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserService _userService;

    public UserController(AppDbContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
    }

    
    }
}