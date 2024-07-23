using EstoqueWEB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Repository
{
    public interface IDevolucaoRepository
    {
        Task<int> UpdateDevolucaoAsync(Devolucao devolucao);
        Task<int> UpdateDevolucao(Devolucao devolucao);
        Task<bool> DeleteDevolucaoAsync(int Id);
        Task<Devolucao> CreateDevolucao(Devolucao devolucao);
        Task<List<Devolucao>> ListDevolucao();
        Task<Devolucao> GetDevolucaoById(int id);
        Task<Devolucao> GetDevolucaoByIdAsync(int id);
        Task<List<Devolucao>> GetDevolucaoByUserId(string userId);

        Task<List<Devolucao>> FilterByStatus(string status);
        Task<List<Devolucao>> GetByUnitAsync(string unit);
    }
}
