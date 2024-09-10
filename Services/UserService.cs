using Catalog_Medical.Models.Entities;
using Catalog_Medical.Models.Requests;
using Catalog_Medical.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Catalog_Medical.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserRegisterRequest request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            Name = request.Name,
            Surname = request.Surname,
            PhoneNumber = request.Phone
        };

        return await _userRepository.AddUserAsync(user, request.Password);
    }

    public async Task<string> LoginUserAsync(UserLoginRequest request)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.UserName);
        if (user == null || !await _userRepository.ValidatePasswordAsync(user, request.Password))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
            }),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = _configuration["Jwt:Audience"],
            Issuer = _configuration["Jwt:Issuer"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<User> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId != null ? await _userRepository.GetUserByIdAsync(userId) : null;
    }

    public bool VerifyToken()
    {
        // Get the token from the Authorization header
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(claim => claim.Type == "nameid")?.Value;

            var user = _userRepository.GetUserByIdAsync(userId).Result;
            if (user == null)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex) {
            return false;
        }
    }

}
