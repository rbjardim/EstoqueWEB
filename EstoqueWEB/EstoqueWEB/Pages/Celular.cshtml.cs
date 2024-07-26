using ClosedXML.Excel;
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
    public class CelularModel : PageModel
    {
        private readonly ICelularService _celularService;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly ILogger<CelularModel> _logger;

        public CelularModel(ICelularService celularService, UserManager<AplicationUser> userManager, ILogger<CelularModel> logger)
        {
            _celularService = celularService ?? throw new ArgumentNullException(nameof(celularService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public Celular celular { get; set; }

        public IList<Celular> ItensDeCelular { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string StatusFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Chamado { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Patrimonio { get; set; }
        public string Unidade { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(Chamado) || !string.IsNullOrEmpty(Patrimonio))
                {
                    ItensDeCelular = await _celularService.SearchByChamado(Chamado);
                }
                else if (!string.IsNullOrEmpty(StatusFiltro))
                {
                    ItensDeCelular = await _celularService.FilterByStatus(StatusFiltro);
                }
                else if (!string.IsNullOrEmpty(Unidade))
                {
                    ItensDeCelular = await _celularService.GetByUnitAsync(Unidade);
                }
                else
                {
                    ItensDeCelular = await _celularService.ListCelular();
                }

                if (ItensDeCelular == null)
                {
                    ItensDeCelular = new List<Celular>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar celulares");
                ItensDeCelular = new List<Celular>();
                TempData["Error"] = "Erro ao carregar celulares: " + ex.Message;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Celular");
                }

                if (string.IsNullOrEmpty(celular.Chamado) || string.IsNullOrEmpty(celular.Nome) || string.IsNullOrEmpty(celular.Patrimonio) || string.IsNullOrEmpty(celular.Modelo) || string.IsNullOrEmpty(celular.Data))
                {
                    TempData["Error"] = "Todos os campos são obrigatórios.";
                    ItensDeCelular = await _celularService.ListCelular();
                    return Page();
                }

                celular.Responsavel = user.Nome;

                await _celularService.CreateCelular(celular);
                TempData["Message"] = "Celular adicionado com sucesso!";
                ItensDeCelular = await _celularService.ListCelular();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar informações de celular");

                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner exception ao salvar informações de celular");
                    TempData["Error"] = $"Erro ao salvar informações: {ex.InnerException.Message}";
                }
                else
                {
                    TempData["Error"] = $"Erro ao salvar informações: {ex.Message}";
                }
            }

            return RedirectToPage("/Celular");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var celularToDelete = await _celularService.GetCelularById(id);
                if (celularToDelete == null)
                {
                    TempData["Error"] = "Celular não encontrado.";
                    return RedirectToPage("/Celular");
                }

                await _celularService.DeleteCelularAsync(id);
                TempData["Message"] = "Celular excluído com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir celular");
                TempData["Error"] = $"Erro ao excluir celular: {ex.Message}";
            }

            return RedirectToPage("/Celular");
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string status)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Celular");
                }

                var celular = await _celularService.GetCelularById(id);
                if (celular == null)
                {
                    TempData["Error"] = "Celular não encontrado.";
                    return RedirectToPage("/Celular");
                }

                celular.Status = status;
                await _celularService.UpdateCelular(celular);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do celular");
                TempData["Error"] = $"Erro ao atualizar status: {ex.Message}";
            }

            return RedirectToPage("/Celular");
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToPage("/Celular");
                }

                var celularToUpdate = await _celularService.GetCelularById(celular.Id);
                if (celularToUpdate == null)
                {
                    TempData["Error"] = "Celular não encontrado.";
                    return RedirectToPage("/Celular");
                }

                celularToUpdate.Chamado = celular.Chamado;
                celularToUpdate.Nome = celular.Nome;
                celularToUpdate.Patrimonio = celular.Patrimonio;
                celularToUpdate.IMEI = celular.IMEI;
                celularToUpdate.Modelo = celular.Modelo;
                celularToUpdate.Data = celular.Data;

                await _celularService.UpdateCelular(celularToUpdate);
                TempData["Message"] = "Celular atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar celular");
                TempData["Error"] = $"Erro ao atualizar celular: {ex.Message}";
            }

            return RedirectToPage("/Celular");
        }

        public async Task<IActionResult> OnGetSearchAsync(string Chamado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Chamado))
                {
                    TempData["Messages"] = "Por favor, informe o número de chamado para busca.";
                    TempData["MessageClass"] = "alert alert-danger";
                    return RedirectToPage("/Celular");
                }

                ItensDeCelular = await _celularService.SearchByChamado(Chamado);

                if (ItensDeCelular == null || !ItensDeCelular.Any())
                {
                    TempData["Messages"] = "Nenhum resultado encontrado.";
                    TempData["MessageClass"] = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar celular por chamado");
                TempData["Error"] = $"Erro ao buscar celular: {ex.Message}";
            }

            return Page();
        }
        public async Task<IActionResult> OnGetSearchByPatrimonioAsync(string Patrimonio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Patrimonio))
                {
                    TempData["Messages"] = "Por favor, informe o número de patrimônio para busca.";
                    TempData["MessageClass"] = "alert alert-danger";
                    return RedirectToPage("/Celular");
                }

                ItensDeCelular = await _celularService.SearchByPatrimonio(Patrimonio);

                if (ItensDeCelular == null || !ItensDeCelular.Any())
                {
                    TempData["Messages"] = "Nenhum resultado encontrado.";
                    TempData["MessageClass"] = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar celular por patrimônio");
                TempData["Error"] = $"Erro ao buscar celular: {ex.Message}";
            }

            return Page();
        }
        public async Task OnGetFilterByUnitAsync(string unidade)
        {
            Unidade = unidade;
            ItensDeCelular = await _celularService.GetByUnitAsync(unidade);
        }
        public async Task<IActionResult> OnGetExportAsync()
        {
            var celulares = await _celularService.ListCelular();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Estoques");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Chamado";
                worksheet.Cell(currentRow, 2).Value = "Nome";
                worksheet.Cell(currentRow, 3).Value = "Patrimônio";
                worksheet.Cell(currentRow, 4).Value = "IMEI";
                worksheet.Cell(currentRow, 5).Value = "Marca/Modelo";
                worksheet.Cell(currentRow, 6).Value = "Data";
                worksheet.Cell(currentRow, 7).Value = "Status";
                worksheet.Cell(currentRow, 8).Value = "Adicionado Por";

                foreach (var celular in celulares)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = celular.Chamado;
                    worksheet.Cell(currentRow, 2).Value = celular.Nome;
                    worksheet.Cell(currentRow, 3).Value = celular.Patrimonio;
                    worksheet.Cell(currentRow, 4).Value = celular.IMEI;
                    worksheet.Cell(currentRow, 5).Value = celular.Modelo;
                    worksheet.Cell(currentRow, 6).Value = celular.Data;
                    worksheet.Cell(currentRow, 7).Value = celular.Status;
                    worksheet.Cell(currentRow, 8).Value = celular.Responsavel;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Celular.xlsx");
                }
            }
        }

    }


}
