using EstoqueWEB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Spire.Doc;
using System;
using System.IO;

namespace EstoqueWEB.Pages
{
    public class LaudoModel : PageModel
    {

        public List<Laudo> Laudo { get; set; } = new List<Laudo>();

        [BindProperty]
        public Laudo NovoLaudo { get; set; }


        [HttpGet]
        public IActionResult GetDocumentContent()
        {

            string documentContent = @"
                <h1>Avaliação Equipamentos</h1>
                <h2>Especificações de Hardware</h2>
                <table border='1' style='border-collapse: collapse; width: 100%;'>
                    <!-- Conteúdo da tabela conforme exemplo acima -->
                </table>
                <h2>Checklist Equipamentos</h2>
                <table border='1' style='border-collapse: collapse; width: 100%;'>
                    <!-- Conteúdo da tabela conforme exemplo acima -->
                </table>
                <!-- Adicione o restante do conteúdo conforme o exemplo acima -->
            ";

            return Content(documentContent, "text/html");
        }

        [HttpPost]
        public IActionResult ExportToPdf([FromBody] DocumentContentModel model)
        {
            string tempPdfFilePath = Path.Combine(Path.GetTempPath(), "LaudoPreenchido.pdf");

            try
            {
                Document document = new Document();
                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(model.Content)))
                {
                    document.LoadFromStream(stream, FileFormat.Html);
                    document.SaveToFile(tempPdfFilePath, FileFormat.PDF);
                }

                byte[] pdfBytes = System.IO.File.ReadAllBytes(tempPdfFilePath);
                return File(pdfBytes, "application/pdf", "LaudoPreenchido.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao exportar para PDF: {ex.Message}");
            }
        }
    }

    public class DocumentContentModel
    {
        public string Content { get; set; }
    }

}