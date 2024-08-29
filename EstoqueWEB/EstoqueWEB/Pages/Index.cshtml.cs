using EstoqueWEB.Model;
using EstoqueWEB.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstoqueWEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly UserManager<AplicationUser> _userManager;

        [BindProperty]
        public Login Model { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SignInManager<AplicationUser> signInManager, UserManager<AplicationUser> userManager)
        {
            _logger = logger;
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
                if (Model.Email == "admin@admin.com.br" && Model.Password == "Ab!123")
                {
                    return RedirectToPage("/Admin");
                }

                var user = await _userManager.FindByEmailAsync(Model.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Model.Password, false, lockoutOnFailure: false);
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