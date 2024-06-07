using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EstoqueWEB.Model;
using EstoqueWEB.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Pages.Admin
{
    public class EstoqueModel : PageModel
    {
        private readonly EstoqueService _estoqueService;

        public List<Estoque> Estoque { get; set; }

        public EstoqueModel(EstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        public async Task OnGet()
        {
            
            Estoque = await _estoqueService.ListEstoque();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _estoqueService.DeleteEstoqueAsync(id);
            return RedirectToPage();
        }

        

        public async Task<IActionResult> OnPostCreateAsync(Estoque estoque)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _estoqueService.CreateEstoque(estoque);
            return RedirectToPage();
        }
    }
}
