using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Model;
using System.Threading.Tasks;

namespace EstoqueWEB.Pages.Local
{
    public class EditDevModel : PageModel
    {
        private readonly IDevolucaoRepository _devolucaoRepository;

        [BindProperty]
        public Estoque Estoque { get; set; }

        public EditDevModel(IDevolucaoRepository devolucaoRepository)
        {
            _devolucaoRepository = devolucaoRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Estoque = await _devolucaoRepository.GetDevolucaoByIdAsync(id);

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

            await _devolucaoRepository.UpdateDevolucaoAsync(Devolucao);

            return RedirectToPage("/Local/Index");
        }
    }
}
