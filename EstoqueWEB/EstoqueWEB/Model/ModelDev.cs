using System.ComponentModel.DataAnnotations;

namespace EstoqueWEB.Model
{
    public class Devolucao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Chamado é obrigatório.")]
        public string Chamado { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Patrimônio é obrigatório.")]
        public string Patrimonio { get; set; }

        [Required(ErrorMessage = "O campo Modelo é obrigatório.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório.")]
        public string Data { get; set; }

        [Required(ErrorMessage = "O campo Chamado Armazenagem é obrigatório.")]
        public string ChamadoArmazenagem { get; set; }

        public string Status { get; set; }



        public Devolucao()
        {

            Modelo = "";
            Status = "Retirar no RH";
        }
    }
}

