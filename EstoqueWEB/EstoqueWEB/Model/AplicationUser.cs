using Microsoft.AspNetCore.Identity;

namespace Estoque.Model
{
    public class AplicationUser : IdentityUser
    {
        public string Nome { get; set; }

    }
}