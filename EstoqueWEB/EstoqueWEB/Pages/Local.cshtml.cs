﻿using ClosedXML.Excel;
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
                    ItensDeEstoque = await _estoqueService.SearchByChamado(Chamado);
                }
                else if (!string.IsNullOrEmpty(StatusFiltro))
                {
                    ItensDeEstoque = await _estoqueService.FilterByStatus(StatusFiltro);
                }
                else if (!string.IsNullOrEmpty(Unidade))
                {
                    ItensDeEstoque = await _estoqueService.GetByUnitAsync(Unidade);
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
                    TempData["Error"] = "Todos os campos são obrigatórios.";
                    ItensDeEstoque = await _estoqueService.ListEstoque();
                    return Page();
                }

                Estoque.Responsavel = user.Nome;


                await _estoqueService.CreateEstoque(Estoque);
                TempData["LocalMessage"] = "Estoque adicionado com sucesso!";
                ItensDeEstoque = await _estoqueService.ListEstoque();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar informações de estoque");
                TempData["Error"] = $"Erro ao salvar informações: {ex.Message}";
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
                    TempData["Error"] = "Estoque não encontrado.";
                    return RedirectToPage("/Local");
                }

                await _estoqueService.DeleteEstoqueAsync(id);
                TempData["LocalMessage"] = "Estoque excluído com sucesso!";
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
                    TempData["Error"] = "Estoque não encontrado.";
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
                    TempData["Error"] = "Estoque não encontrado.";
                    return RedirectToPage("/Local");
                }

                estoqueToUpdate.Chamado = Estoque.Chamado;
                estoqueToUpdate.Nome = Estoque.Nome;
                estoqueToUpdate.Cargo = Estoque.Cargo;
                estoqueToUpdate.Patrimonio = Estoque.Patrimonio;
                estoqueToUpdate.Modelo = Estoque.Modelo;
                estoqueToUpdate.RQ = Estoque.RQ;

                await _estoqueService.UpdateEstoque(estoqueToUpdate);
                TempData["LocalMessage"] = "Estoque atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar item de estoque");
                TempData["Error"] = $"Erro ao atualizar item de estoque: {ex.Message}";
            }

            return RedirectToPage("/Local");
        }

        public async Task<IActionResult> OnGetSearchAsync(string Chamado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Chamado))
                {
                    TempData["Messages"] = "Por favor, informe o número de chamado para busca.";
                    TempData["MessageClass"] = "alert alert-danger";
                    return RedirectToPage("/Local");
                }

                ItensDeEstoque = await _estoqueService.SearchByChamado(Chamado);

                if (ItensDeEstoque == null || !ItensDeEstoque.Any())
                {
                    TempData["Messages"] = "Nenhum resultado encontrado.";
                    TempData["MessageClass"] = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar estoque por chamado");
                TempData["Error"] = $"Erro ao buscar estoque: {ex.Message}";
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
                    return RedirectToPage("/Local");
                }

                ItensDeEstoque = await _estoqueService.SearchByPatrimonio(Patrimonio);

                if (ItensDeEstoque == null || !ItensDeEstoque.Any())
                {
                    TempData["Messages"] = "Nenhum resultado encontrado.";
                    TempData["MessageClass"] = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar estoque por patrimônio");
                TempData["Error"] = $"Erro ao buscar estoque: {ex.Message}";
            }

            return Page();
        }
        public async Task OnGetFilterByUnitAsync(string unidade)
        {
            this.Unidade = Unidade;
            ItensDeEstoque = await _estoqueService.GetByUnitAsync(unidade);
        }

        public async Task<IActionResult> OnGetExportAsync()
        {
            var estoques = await _estoqueService.ListEstoque();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Estoques");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Chamado";
                worksheet.Cell(currentRow, 2).Value = "Nome";
                worksheet.Cell(currentRow, 3).Value = "Cargo";
                worksheet.Cell(currentRow, 4).Value = "Patromônio";
                worksheet.Cell(currentRow, 5).Value = "Modelo";
                worksheet.Cell(currentRow, 6).Value = "RQ";
                worksheet.Cell(currentRow, 7).Value = "Status";
                worksheet.Cell(currentRow, 8).Value = "Adicionado Por";

                foreach (var estoque in estoques)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = estoque.Chamado;
                    worksheet.Cell(currentRow, 2).Value = estoque.Nome;
                    worksheet.Cell(currentRow, 3).Value = estoque.Cargo;
                    worksheet.Cell(currentRow, 4).Value = estoque.Patrimonio;
                    worksheet.Cell(currentRow, 5).Value = estoque.Modelo;
                    worksheet.Cell(currentRow, 6).Value = estoque.RQ;
                    worksheet.Cell(currentRow, 7).Value = estoque.Status;
                    worksheet.Cell(currentRow, 8).Value = estoque.Responsavel;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Estoque.xlsx");
                }
            }
        }
    }


}



