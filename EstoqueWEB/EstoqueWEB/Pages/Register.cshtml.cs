using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EstoqueWEB.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AplicationUser> userManager;
        private readonly SignInManager<AplicationUser> signInManager;
        private readonly Context _dbContext;

        [BindProperty]
        public Register Model { get; set; }
        public class Register
        {
            [Required]
            [Display(Name = "Email Address")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Nome do Usu?rio")]
            public string Nome { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Compare("Password", ErrorMessage = "As senhas n?o conferem.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirme sua Senha")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Telefone")]
            public string Telefone { get; set; }
        }

        public RegisterModel(UserManager<AplicationUser> userManager, SignInManager<AplicationUser> signInManager, Context dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _dbContext = dbContext;
        }


        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new AplicationUser()
                {
                    UserName = Model.Email,
                    Email = Model.Email,
                    Nome = Model.Nome,

                };

                var result = await userManager.CreateAsync(user, Model.Password);
                if (result.Succeeded)
                {

                    await _dbContext.SaveChangesAsync();

                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
    }
}