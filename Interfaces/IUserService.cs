using Catalog_Medical.Models.Entities;
using Catalog_Medical.Models.Requests;
using Microsoft.AspNetCore.Identity;

namespace Catalog_Medical.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegisterRequest request);
        Task<string> LoginUserAsync(UserLoginRequest request);
        Task<User> GetCurrentUserAsync();
        bool VerifyToken();
    }
}
