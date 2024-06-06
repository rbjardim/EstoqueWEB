using System.Threading.Tasks;
using Sustem.Collections.Generic;
using EstoqueWEB.Model


namespace EstoqueWEB.Interface.Repository
{
    public interface IEstoqueRepository
    {
        Task<int> UpdateEstoque(Estoque local);
        Task<bool> DeleteLocalAsync(int Id);
        Task<Estoque> CreateNotebook(Estoque notebook);
        Task<List<Estoque>> ListLocal();
        Task<Estoque> GetLocalizacaoById(int id);
    }
}
