using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueWEB.Model;
using EstoqueWEB.Service;
using Microsoft.AspNetCore.Identity;
using EstoqueWEB.Interface.Service;

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

        public async Task OnGet()
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

    public class UserWithEstoque
    {
        public string UserName { get; set; }
        public List<Estoque> Estoque { get; set; }
    }
}
