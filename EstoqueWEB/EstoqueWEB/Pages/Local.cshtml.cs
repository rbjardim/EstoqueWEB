using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (id != Estoque.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _estoqueService.UpdateEstoque(Estoque);
                if (result == 0)
                {
                    return NotFound();
                }
                TempData["Message"] = "Registro atualizado!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao atualizar informações: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Local");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var result = await _estoqueService.DeleteEstoqueAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                TempData["Message"] = "Registro deletado!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao deletar informações: " + ex.Message;
                return Page();
            }

            return RedirectToPage("/Local");
        }

        public async Task<IActionResult> OnPostExportAsync(int id)
        {
            var estoque = await _estoqueService.GetEstoqueById(id);
            if (estoque == null)
            {
                TempData["Error"] = "Item de estoque não encontrado.";
                return RedirectToPage("/Local");
            }

            var csvContent = new StringBuilder();
            csvContent.AppendLine("ID,Chamado,Nome,Cargo,Patrimonio,Modelo,RQ");
            csvContent.AppendLine($"{estoque.Id},{estoque.Chamado},{estoque.Nome},{estoque.Cargo},{estoque.Patrimonio},{estoque.Modelo},{estoque.RQ}");

            byte[] buffer = Encoding.UTF8.GetBytes(csvContent.ToString());
            return File(buffer, "text/csv", "estoque.csv");
        }
    }
}
