using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly UserManager<AplicationUser> _userManager;

    public UserService(UserManager<AplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<AplicationUser>> GetAllUsersAsync()
    {
        return _userManager.Users.ToList();
    }

    public async Task<AplicationUser> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task DeleteUserAsync(string id)
    {
        var user = await GetUserByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
    }
    public async Task<IdentityResult> AddUserToRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Usuário não encontrado." });

        return await _userManager.AddToRoleAsync(user, role);
    }

    public Task<IActionResult> OnPostPromoteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }
}

