using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task OnGetAsync()
        {
            await LoadUsersWithEstoqueAsync();
        }

        private async Task LoadUsersWithEstoqueAsync()
        {
            var users = await _userManager.Users.ToListAsync(); // Correção: Use ToListAsync() aqui
            UsersWithEstoque = new List<UserWithEstoque>();

            foreach (var user in users)
            {
                var userWithEstoque = new UserWithEstoque
                {
                    UserName = user.UserName,
                    EstoqueItems = await _estoqueService.GetEstoqueByUserId(user.Id).ToListAsync() // Correção: Use ToListAsync() aqui
                };
                UsersWithEstoque.Add(userWithEstoque);
            }
        }
    }
}
