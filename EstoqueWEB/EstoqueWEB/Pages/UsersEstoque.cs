using EstoqueWEB.Model;
using System.Collections.Generic;

namespace EstoqueWEB.Pages
{
    public class UserWithEstoque
    {
        public string UserName { get; set; }
        public List<Estoque> Estoque { get; set; }
    }
}
