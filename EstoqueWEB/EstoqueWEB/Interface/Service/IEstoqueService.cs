using EstoqueWEB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Service
{
    public interface IEstoqueService
    {
        Task<bool> CreateEstoque(Estoque estoque);
        Task<bool> DeleteEstoqueAsync(int id);
        Task<Estoque> GetEstoqueById(int id);
        Task<List<Estoque>> ListEstoque();
        Task<int> UpdateEstoque(Estoque estoque);
        Task<List<Estoque>> FilterByStatus(string status);
        Task<List<Estoque>> SearchByChamado(string chamado);
        Task<List<Estoque>> SearchByPatrimonio(string patrimonio);
        Task<List<Estoque>> GetByUnitAsync(string unit);
        Task AddAsync(Estoque estoque);
    }
}
