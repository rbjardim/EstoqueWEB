using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueWEB.Pages
{
    public class AdminUserModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<AdminModel> _logger;

        public AdminUserModel(IUserService userService, ILogger<AdminModel> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IList<AplicationUser> Users { get; private set; }

        public int TotalUsuarios { get; private set; }


        public async Task OnGetAsync()
        {
            Users = await _userService.GetAllUsersAsync();

        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                TempData["AdminMessage"] = "Usuário excluído com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir usuário");
                TempData["Error"] = "Erro ao excluir usuário: " + ex.Message;
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostPromoteUserAsync(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();

            var result = await _userService.AddUserToRoleAsync(userId, "Admin");

            if (result.Succeeded)
            {
                TempData["AdminMessage"] = "Usuário promovido a administrador com sucesso!";
            }
            else
            {
                TempData["AdminError"] = "Erro ao promover usuário: " + string.Join(", ", result.Errors.Select(e => e.Description));
            }

            return RedirectToPage("/Admin");
        }
    }
}

