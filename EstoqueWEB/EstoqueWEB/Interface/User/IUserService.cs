using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Service
{
    public interface IUserService
    {
        Task<List<AplicationUser>> GetAllUsersAsync();
        Task<AplicationUser> GetUserByIdAsync(string id);
        Task DeleteUserAsync(string id);
        Task<IActionResult> OnPostPromoteUserAsync(string userId);
        Task<IdentityResult> AddUserToRoleAsync(string userId, string role);
    }
}
