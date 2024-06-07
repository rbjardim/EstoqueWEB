using System.Threading.Tasks;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;

namespace EstoqueWEB.Service
{
    public class UserService
    {
        private readonly UserManager<AplicationUser> _userManager;

        public UserService(UserManager<AplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<AplicationUser>> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<AplicationUser> GetUserById(string userId)
        {
            
            return null;
        }

        public async Task<AplicationUser> GetUserByEmail(string email)
        {
           
            return null; 
        }

        public async Task<bool> CreateUser(AplicationUser user, string password)
        {
          
            return false; 
        }

        public async Task<bool> UpdateUser(AplicationUser user)
        {
            
            return false;
        }

        public async Task<bool> DeleteUser(string userId)
        {
           
            return false; 
        }

  
    }
}
