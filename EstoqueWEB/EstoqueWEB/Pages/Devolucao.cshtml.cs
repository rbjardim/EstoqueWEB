using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using EstoqueWEB.Service.EstoqueWEB.Service;
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
    public class DevolucaoModel : PageModel
    {
        private readonly IDevolucaoService _devolucaoService;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly ILogger<DevolucaoModel> _logger;

        public DevolucaoModel(IDevolucaoService devolucaoService, UserManager<AplicationUser> userManager, ILogger<DevolucaoModel> logger)
        {
            _devolucaoService = devolucaoService ?? throw new ArgumentNullException(nameof(devolucaoService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public Devolucao Devolucao { get; set; }

        public IList<Devolucao> ItensDeDevolucao { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string StatusFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Chamado { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Patrimonio { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(Chamado) || !string.IsNullOrEmpty(Patrimonio))
                {
                    ItensDeDevolucao = await _devolucaoService.SearchByChamado(Chamado);
                }
                else if (!string.IsNullOrEmpty(StatusFiltro))
                {
                    ItensDeDevolucao = await _devolucaoService.FilterByStatus(StatusFiltro);
                }
                else
                {
                    ItensDeDevolucao = await _devolucaoService.ListDevolucao();
                }

                if (ItensDeDevolucao == null)
                {
                    ItensDeDevolucao = new List<Devolucao>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar itens de devolu��o");
                ItensDeDevolucao = new List<Devolucao>();
                TempData["Error"] = "Erro ao carregar itens de devolu��o: " + ex.Message;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Devolucao");
                }

                if (string.IsNullOrEmpty(Devolucao.Chamado) || string.IsNullOrEmpty(Devolucao.Nome) || string.IsNullOrEmpty(Devolucao.Patrimonio) || string.IsNullOrEmpty(Devolucao.Data) || string.IsNullOrEmpty(Devolucao.Modelo) || string.IsNullOrEmpty(Devolucao.ChamadoArmazenagem))
                {
                    TempData["Error"] = "Todos os campos s�o obrigat�rios.";
                    ItensDeDevolucao = await _devolucaoService.ListDevolucao();
                    return Page();
                }

                await _devolucaoService.CreateDevolucao(Devolucao);
                TempData["Message"] = "Item de devolu��o adicionado com sucesso!";
                ItensDeDevolucao = await _devolucaoService.ListDevolucao();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar informa��es de devolu��o");

                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner exception ao salvar informa��es de devolu��o");
                    TempData["Error"] = $"Erro ao salvar informa��es: {ex.InnerException.Message}";
                }
                else
                {
                    TempData["Error"] = $"Erro ao salvar informa��es: {ex.Message}";
                }
            }

            return RedirectToPage("/Devolucao");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var devolucaoToDelete = await _devolucaoService.GetDevolucaoById(id);
                if (devolucaoToDelete == null)
                {
                    TempData["Error"] = "Item de devolu��o n�o encontrado.";
                    return RedirectToPage("/Devolucao");
                }

                await _devolucaoService.DeleteDevolucaoAsync(id);
                TempData["Message"] = "Item de devolu��o exclu�do com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir item de devolu��o");
                TempData["Error"] = $"Erro ao excluir item de devolu��o: {ex.Message}";
            }

            return RedirectToPage("/Devolucao");
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string status)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Devolucao");
                }

                var devolucao = await _devolucaoService.GetDevolucaoById(id);
                if (devolucao == null)
                {
                    TempData["Error"] = "Item de devolu��o n�o encontrado.";
                    return RedirectToPage("/Devolucao");
                }

                devolucao.Status = status;
                await _devolucaoService.UpdateDevolucao(devolucao);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do devolu��o");
                TempData["Error"] = $"Erro ao atualizar status: {ex.Message}";
            }

            return RedirectToPage("/Devolucao");
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Devolucao");
                }

                var devolucaoToUpdate = await _devolucaoService.GetDevolucaoById(Devolucao.Id);
                if (devolucaoToUpdate == null)
                {
                    TempData["Error"] = "Item de devolu��o n�o encontrado.";
                    return RedirectToPage("/Devolucao");
                }

                devolucaoToUpdate.Chamado = Devolucao.Chamado;
                devolucaoToUpdate.Nome = Devolucao.Nome;
                devolucaoToUpdate.Patrimonio = Devolucao.Patrimonio;
                devolucaoToUpdate.Data = Devolucao.Data;
                devolucaoToUpdate.Modelo = Devolucao.Modelo;
                devolucaoToUpdate.ChamadoArmazenagem = Devolucao.ChamadoArmazenagem;

                await _devolucaoService.UpdateDevolucao(devolucaoToUpdate);
                TempData["Message"] = "Item de devolu��o atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar item de devolu��o");
                TempData["Error"] = $"Erro ao atualizar item de devolu��o: {ex.Message}";
            }

            return RedirectToPage("/Devolucao");
        }

        public async Task<IActionResult> OnGetSearchAsync(string Chamado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Chamado))
                {
                    TempData["Messages"] = "Por favor, informe o n�mero de chamado para busca.";
                    TempData["MessageClass"] = "alert alert-danger";
                    return RedirectToPage("/Devolucao");
                }

                ItensDeDevolucao = await _devolucaoService.SearchByChamado(Chamado);

                if (ItensDeDevolucao == null || !ItensDeDevolucao.Any())
                {
                    TempData["Messages"] = "Nenhum resultado encontrado.";
                    TempData["MessageClass"] = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar itens de devolu��o por chamado");
                TempData["Error"] = $"Erro ao buscar itens de devolu��o: {ex.Message}";
            }

            return Page();
        }
        public async Task<IActionResult> OnGetSearchByPatrimonioAsync(string Patrimonio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Patrimonio))
                {
                    TempData["Messages"] = "Por favor, informe o n�mero de patrim�nio para busca.";
                    TempData["MessageClass"] = "alert alert-danger";
                    return RedirectToPage("/Devolucao");
                }

                ItensDeDevolucao = await _devolucaoService.SearchByPatrimonio(Patrimonio);

                if (ItensDeDevolucao == null || !ItensDeDevolucao.Any())
                {
                    TempData["Messages"] = "Nenhum resultado encontrado.";
                    TempData["MessageClass"] = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar itens de devolu��o por patrim�nio");
                TempData["Error"] = $"Erro ao buscar itens de devolu��o: {ex.Message}";
            }

            return Page();
        }

    }

}
