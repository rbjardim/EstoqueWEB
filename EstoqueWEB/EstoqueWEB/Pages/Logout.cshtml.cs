using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EstoqueWEB.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AplicationUser> _signInManager;

        public LogoutModel(SignInManager<AplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostDontLogoutAsync()
        {
            return RedirectToPage("/Index");
        }
    }
}
