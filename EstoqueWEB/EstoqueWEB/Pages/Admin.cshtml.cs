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
    public class AdminModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IEstoqueService _estoqueService;
        private readonly IDevolucaoService _devolucaoService;
        private readonly ICelularService _celularService;
        private readonly ILogger<AdminModel> _logger;
        private readonly UserManager<AplicationUser> _userManager;

        public AdminModel(IUserService userService, IEstoqueService estoqueService, IDevolucaoService devolucaoService, ICelularService celularService, ILogger<AdminModel> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _estoqueService = estoqueService ?? throw new ArgumentNullException(nameof(estoqueService));
            _devolucaoService = devolucaoService ?? throw new ArgumentNullException(nameof(devolucaoService));
            _celularService = celularService ?? throw new ArgumentNullException(nameof(celularService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public Estoque Estoque { get; set; }
       
        [BindProperty(SupportsGet = true)]
        public string StatusFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Chamado { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Patrimonio { get; set; }
        public string Unidade { get; set; }
        public IList<AplicationUser> Users { get; private set; }
        public IList<Estoque> ItensDeEstoque { get; private set; }
        public IList<Devolucao> ItensDeDevolucao { get; private set; }
        public IList<Celular> ItensDeCelular { get; private set; }

        public int TotalUsuarios { get; private set; }
        public int TotalItensEstoque { get; private set; }
        public int TotalItensDevolucao { get; private set; }
        public int TotalItensCelular { get; private set; }

        public async Task OnGetAsync()
        {
            Users = await _userService.GetAllUsersAsync();
            ItensDeEstoque = await _estoqueService.ListEstoque();
            ItensDeDevolucao = await _devolucaoService.ListDevolucao();
            ItensDeCelular = await _celularService.ListCelular();

            TotalUsuarios = Users.Count;
            TotalItensEstoque = ItensDeEstoque.Count;
            TotalItensDevolucao = ItensDeDevolucao.Count;
            TotalItensCelular = ItensDeCelular.Count;
        }

        public async Task<IActionResult> OnPostDeleteUserAsync(string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                TempData["Admin2Message"] = "Usuário excluído com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir usuário");
                TempData["Admin2Error"] = "Erro ao excluir usuário: " + ex.Message;
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
                TempData["Admin2MessageEstoque"] = "Item de Estoque excluído com sucesso!";
                isEstoqueDeleted = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir item de estoque");
                TempData["Admin2ErrorEstoque"] = "Erro ao excluir item de estoque: " + ex.Message;
            }

            try
            {
                await _devolucaoService.DeleteDevolucaoAsync(id);
                TempData["Admin2MessageDevolucao"] = "Item de Devolução excluído com sucesso!";
                isDevolucaoDeleted = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir item de devolução");
                TempData["Admin2ErrorDevolucao"] = "Erro ao excluir item de devolução: " + ex.Message;
            }

            if (!isEstoqueDeleted && !isDevolucaoDeleted)
            {
                TempData["Admin2Error"] = "Erro ao excluir item: nenhum dos itens foi excluído com sucesso.";
            }

            return RedirectToPage();
        }

    }

}

