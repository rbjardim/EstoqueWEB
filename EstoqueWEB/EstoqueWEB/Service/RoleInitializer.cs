using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;

namespace EstoqueWEB.Service
{
    public class RoleInitializer : IRoleInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AplicationUser> _userManager;

        public RoleInitializer(RoleManager<IdentityRole> roleManager, UserManager<AplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitializeRoles()
        {
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await _userManager.FindByEmailAsync("admin@example.com");
            if (adminUser != null)
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
