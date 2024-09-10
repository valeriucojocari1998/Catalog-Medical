using Catalog_Medical.Models.Requests;
using Catalog_Medical.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_Medical.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.RegisterUserAsync(request);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { Message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var token = await _userService.LoginUserAsync(request);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        return Ok(new { Token = token });
    }

    [HttpGet("user-details")]
    public async Task<IActionResult> GetUserDetails()
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            return Unauthorized(new { Message = "User not found" });
        }

        return Ok(user);
    }

    [HttpGet("verify-token")]
    public IActionResult VerifyToken()
    {
        var isValid = _userService.VerifyToken();
        if (!isValid)
        {
            return Unauthorized(new { Message = "Token is invalid or expired" });
        }

        return Ok(new { Message = "Token is valid" });
    }
}
