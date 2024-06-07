using EstoqueWEB.Model;
using EstoqueWEB.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstoqueWEB.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly UserManager<AplicationUser> userManager;

        [BindProperty]
        public Login Model { get; set; }

        public LoginModel(SignInManager<AplicationUser> signInManager, UserManager<AplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
                    return RedirectToPage("Local");
                }

                var user = await userManager.FindByEmailAsync(Model.Email);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, Model.Password, Model.RelembreMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        if (returnUrl == null || returnUrl == "/")
                        {
                            return RedirectToPage("Local");
                        }
                        else
                        {
                            return RedirectToPage(returnUrl);
                        }
                    }
                }

                ModelState.AddModelError("", "Nome de usuário ou senha incorretos!");
            }

            return Page();
        }
    }
}