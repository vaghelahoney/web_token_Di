using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using web_token_Di.Models.DTOs;

namespace web_token_Di.Repositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterModel model);
        Task<string> LoginAsync(LoginModel model);
    }
}
