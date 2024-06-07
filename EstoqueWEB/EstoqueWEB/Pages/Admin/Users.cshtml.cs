using EstoqueWEB.Model;
using EstoqueWEB.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace EstoqueWEB.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly UserService _userService;

        public List<AplicationUser> Users { get; set; }

        public UsersModel(UserService userService)
        {
            _userService = userService;
        }

        public async Task OnGet()
        {
            
            Users = await _userService.GetAllUsers();
        }


    }
}
