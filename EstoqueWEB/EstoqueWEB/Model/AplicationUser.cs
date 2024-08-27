using Microsoft.AspNetCore.Identity;

namespace EstoqueWEB.Model
{
    public class AplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public bool IsAdmin { get; set; }

        public async Task<bool> CheckIfUserIsAdmin(UserManager<AplicationUser> userManager)
        {
            var roles = await userManager.GetRolesAsync(this);
            return roles.Contains("Admin");
        }
    }
}