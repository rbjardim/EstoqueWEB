using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<LocalModel> _logger;

        public LocalModel(IEstoqueService estoqueService, UserManager<AplicationUser> userManager, ILogger<LocalModel> logger)
        {
            _estoqueService = estoqueService ?? throw new ArgumentNullException(nameof(estoqueService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public Estoque Estoque { get; set; }

        public IList<Estoque> ItensDeEstoque { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string StatusFiltro { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(StatusFiltro))
                {
                    ItensDeEstoque = await _estoqueService.FilterByStatus(StatusFiltro);
                }
                else
                {
                    ItensDeEstoque = await _estoqueService.ListEstoque();
                }

                if (ItensDeEstoque == null)
                {
                    ItensDeEstoque = new List<Estoque>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar itens de estoque");
                ItensDeEstoque = new List<Estoque>();
                TempData["Error"] = "Erro ao carregar itens de estoque: " + ex.Message;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Local");
                }

                if (string.IsNullOrEmpty(Estoque.Chamado) || string.IsNullOrEmpty(Estoque.Nome) || string.IsNullOrEmpty(Estoque.Cargo) || string.IsNullOrEmpty(Estoque.Modelo) || string.IsNullOrEmpty(Estoque.RQ))
                {
                    TempData["Error"] = "Todos os campos s�o obrigat�rios.";
                    ItensDeEstoque = await _estoqueService.ListEstoque();
                    return Page();
                }

                await _estoqueService.CreateEstoque(Estoque);
                TempData["Message"] = "Registro salvo!";
                ItensDeEstoque = await _estoqueService.ListEstoque();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar informa��es de estoque");
                TempData["Error"] = $"Erro ao salvar informa��es: {ex.Message}";
            }

            return RedirectToPage("/Local");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var estoqueToDelete = await _estoqueService.GetEstoqueById(id);
                if (estoqueToDelete == null)
                {
                    TempData["Error"] = "Item de estoque n�o encontrado.";
                    return RedirectToPage("/Local");
                }

                await _estoqueService.DeleteEstoqueAsync(id);
                TempData["Message"] = "Item de estoque exclu�do com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir item de estoque");
                TempData["Error"] = $"Erro ao excluir item de estoque: {ex.Message}";
            }

            return RedirectToPage("/Local");
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string status)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Local");
                }

                var estoque = await _estoqueService.GetEstoqueById(id);
                if (estoque == null)
                {
                    TempData["Error"] = "Item de estoque n�o encontrado.";
                    return RedirectToPage("/Local");
                }

                estoque.Status = status;
                await _estoqueService.UpdateEstoque(estoque);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do estoque");
                TempData["Error"] = $"Erro ao atualizar status: {ex.Message}";
            }

            return RedirectToPage("/Local");
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Local");
                }

                var estoqueToUpdate = await _estoqueService.GetEstoqueById(Estoque.Id);
                if (estoqueToUpdate == null)
                {
                    TempData["Error"] = "Item de estoque n�o encontrado.";
                    return RedirectToPage("/Local");
                }

                estoqueToUpdate.Chamado = Estoque.Chamado;
                estoqueToUpdate.Nome = Estoque.Nome;
                estoqueToUpdate.Cargo = Estoque.Cargo;
                estoqueToUpdate.Patrimonio = Estoque.Patrimonio;
                estoqueToUpdate.Modelo = Estoque.Modelo;
                estoqueToUpdate.RQ = Estoque.RQ;

                await _estoqueService.UpdateEstoque(estoqueToUpdate);
                TempData["Message"] = "Item de estoque atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar item de estoque");
                TempData["Error"] = $"Erro ao atualizar item de estoque: {ex.Message}";
            }

            return RedirectToPage("/Local");
        }
    }
}
