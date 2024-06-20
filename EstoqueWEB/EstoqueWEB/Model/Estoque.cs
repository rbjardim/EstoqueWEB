using System.ComponentModel.DataAnnotations;

namespace EstoqueWEB.Model
{
    public class Estoque
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Chamado é obrigatório.")]
        public string Chamado { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Cargo é obrigatório.")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "O campo Patrimônio é obrigatório.")]
        public string Patrimonio { get; set; }

        [Required(ErrorMessage = "O campo Modelo é obrigatório.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo RQ é obrigatório.")]
        public string RQ { get; set; }

        public string Status { get; set; }

        public int Quantidade { get; set; }

        public Estoque()
        {

            Modelo = "";
            Status = "Configurando";
        }
    }
}
