using System.ComponentModel.DataAnnotations;

namespace EstoqueWEB.Model
{
    public class Estoque
    {
        public int Id { get; set; }
        public string Chamado { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Patrimonio { get; set; }
        public string Modelo { get; set; }
        public string RQ { get; set; }
        public string Status { get; set; } = "Configurando";
        public int Quantidade { get; set; }
    }
}