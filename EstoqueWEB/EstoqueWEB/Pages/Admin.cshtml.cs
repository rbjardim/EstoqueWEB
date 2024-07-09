using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IEstoqueService _estoqueService;
        private readonly IDevolucaoService _devolucaoService;
        private readonly ILogger<AdminModel> _logger;

        public AdminModel(IUserService userService, IEstoqueService estoqueService, IDevolucaoService devolucaoService, ILogger<AdminModel> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _estoqueService = estoqueService ?? throw new ArgumentNullException(nameof(estoqueService));
            _devolucaoService = devolucaoService ?? throw new ArgumentNullException(nameof(devolucaoService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IList<AplicationUser> Users { get; private set; }
        public IList<Estoque> ItensDeEstoque { get; private set; }

        public IList<Devolucao> ItensDeDevolucao { get; private set; }
        public async Task OnGetAsync()
        {
            Users = await _userService.GetAllUsersAsync();
            ItensDeEstoque = await _estoqueService.ListEstoque();
            ItensDeDevolucao = await _devolucaoService.ListDevolucao();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                TempData["Message"] = "Usuário excluído com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir usuário");
                TempData["Error"] = "Erro ao excluir usuário: " + ex.Message;
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteItemAsync(int id)
        {
            bool isEstoqueDeleted = false;
            bool isDevolucaoDeleted = false;

            try
            {
                await _estoqueService.DeleteEstoqueAsync(id);
                TempData["MessageEstoque"] = "Item de Estoque excluído com sucesso!";
                isEstoqueDeleted = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir item de estoque");
                TempData["ErrorEstoque"] = "Erro ao excluir item de estoque: " + ex.Message;
            }

            try
            {
                await _devolucaoService.DeleteDevolucaoAsync(id);
                TempData["MessageDevolucao"] = "Item de Devolução excluído com sucesso!";
                isDevolucaoDeleted = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir item de devolução");
                TempData["ErrorDevolucao"] = "Erro ao excluir item de devolução: " + ex.Message;
            }

            if (!isEstoqueDeleted && !isDevolucaoDeleted)
            {
                TempData["Error"] = "Erro ao excluir item: nenhum dos itens foi excluído com sucesso.";
            }

            return RedirectToPage();
        }


    }
}
