using EstoqueWEB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Service
{
    public interface IEstoqueService
    {
        Task<bool> CreateEstoque(Estoque estoque);
        Task<bool> DeleteEstoqueAsync(int Idlocal);
        Task<Estoque> GetEstoqueById(int id);
        Task<List<Estoque>> ListEstoque();
        Task<int> UpdateEstoque(Estoque estoque);

    }
}