using System.Threading.Tasks;
using System.Collections.Generic;
using EstoqueWEB.Model;


namespace EstoqueWEB.Interface.Repository
{
    public interface IEstoqueRepository
    {
        Task<int> UpdateEstoque(Estoque local);
        Task<bool> DeleteEstoqueAsync(int Id);
        Task<Estoque> CreateEstoque(Estoque notebook);
        Task<List<Estoque>> ListEstoque();
        Task<Estoque> GetEstoqueById(int id);
    }
}
