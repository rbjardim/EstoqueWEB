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
        private readonly UserManager<AplicationUser> _userManager;

        public LogoutModel(SignInManager<AplicationUser> signInManager, UserManager<AplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostDontLogoutAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {

                return RedirectToPage("/Index");
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToPage("/Admin");
            }

            return RedirectToPage("/Local");
        }
    }
}
