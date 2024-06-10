using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Model;
using System.Threading.Tasks;

namespace EstoqueWEB.Pages.Local
{
    public class EditModel : PageModel
    {
        private readonly IEstoqueRepository _estoqueRepository;

        [BindProperty]
        public Estoque Estoque { get; set; }

        public EditModel(IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository = estoqueRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Estoque = await _estoqueRepository.GetEstoqueByIdAsync(id);

            if (Estoque == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _estoqueRepository.UpdateEstoqueAsync(Estoque);

            return RedirectToPage("/Local/Index");
        }
    }
}
