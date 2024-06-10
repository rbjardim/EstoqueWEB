using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstoqueWEB.Pages
{
    public class LocalModel : PageModel
    {
        private readonly IEstoqueService _estoqueService;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly ILogger<LocalModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalModel(IEstoqueService estoqueService, UserManager<AplicationUser> userManager, ILogger<LocalModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _estoqueService = estoqueService ?? throw new ArgumentNullException(nameof(estoqueService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        [BindProperty]
        public Estoque Estoque { get; set; }
        public IList<Estoque> ItensDeEstoque { get; private set; }

        public async Task OnGetAsync()
        {
            try
            {
                ItensDeEstoque = await _estoqueService.ListEstoque();
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

        public async Task<IActionResult> OnGetDetailsAsync(int id)
        {
            try
            {
                ItensDeEstoque = await _estoqueService.ListEstoque();
                if (ItensDeEstoque == null)
                {
                    ItensDeEstoque = new List<Estoque>();
                    TempData["Error"] = "Não foi possível carregar os itens de estoque.";
                    return Page();
                }
                Estoque = ItensDeEstoque.FirstOrDefault(e => e.Id == id);
                if (Estoque == null)
                {
                    return NotFound();
                }
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar detalhes do item de estoque");
                TempData["Error"] = "Erro ao carregar detalhes do item de estoque: " + ex.Message;
                return Page();
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

                if (string.IsNullOrEmpty(Estoque.Patrimonio))
                {
                    TempData["Error"] = "O campo 'Patrimônio' é obrigatório.";
                    return Page();
                }


                Estoque.Status = Request.Form["status"];


                if (string.IsNullOrEmpty(Estoque.Status))
                {
                    Estoque.Status = "Pendente";
                }

                await _estoqueService.CreateEstoque(Estoque);
                TempData["Message"] = "Registro salvo!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar informações de estoque");

                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "Detalhes adicionais não disponíveis.";

                TempData["Error"] = $"Erro ao salvar informações: {innerExceptionMessage}";
                return Page();
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
                    TempData["Error"] = "Item de estoque não encontrado.";
                    return RedirectToPage("/Local");
                }

                await _estoqueService.DeleteEstoqueAsync(id);

                TempData["Message"] = "Item de estoque excluído com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir item de estoque");

                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "Detalhes adicionais não disponíveis.";

                TempData["Error"] = $"Erro ao excluir item de estoque: {innerExceptionMessage}";
            }

            return RedirectToPage("/Local");
        }
    }
}
