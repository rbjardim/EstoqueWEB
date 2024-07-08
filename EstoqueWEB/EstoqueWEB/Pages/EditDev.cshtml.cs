using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstoqueWEB.Pages
{
    public class EditDevModel : PageModel
    {
        private readonly IDevolucaoRepository _devolucaoRepository;

        [BindProperty]
        public Devolucao Devolucao { get; set; }

        public EditDevModel(IDevolucaoRepository devolucaoRepository)
        {
            _devolucaoRepository = devolucaoRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Devolucao = await _devolucaoRepository.GetDevolucaoByIdAsync(id);

            if (Devolucao == null)
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

            return RedirectToPage("/Devolucao/Index");
        }
    }
}