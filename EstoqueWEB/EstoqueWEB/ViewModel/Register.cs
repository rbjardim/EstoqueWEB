using System.ComponentModel.DataAnnotations;

namespace EstoqueWEB.ViewModel
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Senha errada!")]
        public string ConfirmPassword { get; set; }
    }
}