using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;

        public UserHelper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
    }
}
