using Microsoft.AspNetCore.Identity;

namespace EstoqueWEB.Model
{
    public class AplicationUser : IdentityUser
    {
        public string Nome { get; set; }

    }
}