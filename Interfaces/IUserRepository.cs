using Catalog_Medical.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Catalog_Medical.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(string id);
        Task<bool> ValidatePasswordAsync(User user, string password);
    }
}
