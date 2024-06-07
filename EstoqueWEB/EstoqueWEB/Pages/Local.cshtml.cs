using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueWEB.Pages
{
    public class LocalModel : PageModel
    {
        private readonly IEstoqueService _estoqueService;
        private readonly UserManager<AplicationUser> _userManager;

        public LocalModel(IEstoqueService estoqueService, UserManager<AplicationUser> userManager)
        {
            _estoqueService = estoqueService;
            _userManager = userManager;
        }

        [BindProperty]
        public Estoque Estoque { get; set; }
        public IList<Estoque> ItensDeEstoque { get; private set; }

        public async Task OnGetAsync()
        {
            ItensDeEstoque = await _estoqueService.ListEstoque();
        }

        public async Task<IActionResult> OnGetDetailsAsync(int id)
        {
            ItensDeEstoque = await _estoqueService.ListEstoque();
            Estoque = ItensDeEstoque.FirstOrDefault(e => e.Id == id);
            if (Estoque == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Local");
            }

            try
            {
                await _estoqueService.CreateEstoque(Estoque);
                TempData["Message"] = "Registro salvo!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao salvar informações: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Local");
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id)
        {
            var estoqueToUpdate = await _estoqueService.GetEstoqueById(id);
            if (estoqueToUpdate == null)
            {
                return NotFound();
            }

            string status = Request.Form["status"];

            estoqueToUpdate.Status = status;

            try
            {
                await _estoqueService.UpdateEstoque(estoqueToUpdate);
                TempData["Message"] = "Status atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao atualizar status: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Local");
        }
    }
}
