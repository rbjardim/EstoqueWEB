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
            try
            {
                await _estoqueService.DeleteEstoqueAsync(id);
                TempData["Message"] = "Item de Devolução excluído com sucesso!";
                await _devolucaoService.DeleteDevolucaoAsync(id);
                TempData["Message"] = "Item de Devolução excluído com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir item de devolução");
                TempData["Error"] = "Erro ao excluir item de devolução: " + ex.Message;
            }

            return RedirectToPage();
        }
    }
}
