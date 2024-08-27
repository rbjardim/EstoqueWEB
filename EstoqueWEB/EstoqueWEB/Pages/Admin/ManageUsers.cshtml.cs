using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


public class ManageUsersModel : PageModel
{
    private readonly UserManager<AplicationUser> _userManager;

    public ManageUsersModel(UserManager<AplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public List<AplicationUser> Users { get; set; }

    public async Task OnGetAsync()
    {
        Users = await _userManager.Users.ToListAsync();
    }

    public async Task<IActionResult> OnPostChangeUserRoleAsync(string userId, string newRole)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRoleAsync(user, newRole);


        user.Role = newRole;
        await _userManager.UpdateAsync(user);

        return RedirectToPage();
    }
}
