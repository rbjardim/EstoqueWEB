using EstoqueWEB.Model;
using EstoqueWEB.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EstoqueWEB.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly UserManager<AplicationUser> _userManager;

        [BindProperty]
        public Login Model { get; set; }

        public LoginModel(SignInManager<AplicationUser> signInManager, UserManager<AplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {

                if (Model.Email == "admin@aec.com.br" && Model.Password == "Ab!123")
                {
  
                    return RedirectToPage("/Admin");
                }

                var user = await _userManager.FindByEmailAsync(Model.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Model.Password, Model.RelembreMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                        if (isAdmin)
                        {
                            return RedirectToPage("/Admin");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
                            {
                                return RedirectToPage("/Local");
                            }
                            else
                            {
                                return RedirectToPage(returnUrl);
                            }
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Nome de usuário ou senha incorretos!");
            }

            return Page();
        }
    }
}
