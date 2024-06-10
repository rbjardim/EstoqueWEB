using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;

namespace EstoqueWEB.Pages
{
    public class AdminModel : PageModel
    {
        private readonly UserManager<AplicationUser> _userManager;
        private readonly IEstoqueService _estoqueService;

        public AdminModel(UserManager<AplicationUser> userManager, IEstoqueService estoqueService)
        {
            _userManager = userManager;
            _estoqueService = estoqueService;
        }

        public List<UserWithEstoque> UsersWithEstoque { get; set; }
        public bool IsDeleteConfirmed { get; set; }

        public async Task OnGet()
        {
            await LoadUsersWithEstoqueAsync();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string userName)
        {
            if (IsDeleteConfirmed)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                    await LoadUsersWithEstoqueAsync();
                    return RedirectToPage();
                }
                return NotFound();
            }
            else
            {
                return Page();
            }
        }

        private async Task LoadUsersWithEstoqueAsync()
        {
            var users = _userManager.Users.ToList();
            UsersWithEstoque = new List<UserWithEstoque>();

            foreach (var user in users)
            {
                var userWithEstoque = new UserWithEstoque
                {
                    UserName = user.UserName,
                    Estoque = await _estoqueService.GetEstoqueByUserId(user.Id)
                };
                UsersWithEstoque.Add(userWithEstoque);
            }
        }
    }
}
