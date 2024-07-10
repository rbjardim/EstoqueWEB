using EstoqueWEB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Service
{
    public interface IDevolucaoService
    {
        Task<bool> CreateDevolucao(Devolucao devolucao);
        Task<bool> DeleteDevolucaoAsync(int id);
        Task<Devolucao> GetDevolucaoById(int id);
        Task<List<Devolucao>> ListDevolucao();
        Task<int> UpdateDevolucao(Devolucao devolucao2);
        Task<List<Devolucao>> FilterByStatus(string status);
        Task<List<Devolucao>> SearchByChamado(string chamado);
        Task<List<Devolucao>> SearchByPatrimonio(string patrimonio);
        Task AddAsync(Devolucao devolucao);
    }
}
