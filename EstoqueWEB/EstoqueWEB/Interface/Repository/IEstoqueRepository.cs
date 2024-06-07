using System.Threading.Tasks;
using System.Collections.Generic;
using EstoqueWEB.Model;


namespace EstoqueWEB.Interface.Repository
{
    public interface IEstoqueRepository
    {
        Task<int> UpdateEstoque(Estoque estoque);
        Task<bool> DeleteEstoqueAsync(int Id);
        Task<Estoque> CreateEstoque(Estoque estoque);
        Task<List<Estoque>> ListEstoque();
        Task<Estoque> GetEstoqueById(int id);
    }
}
