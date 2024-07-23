using EstoqueWEB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Service
{
    public interface ICelularService
    {
        Task<bool> CreateCelular(Celular celular);
        Task<bool> DeleteCelularAsync(int id);
        Task<Celular> GetCelularById(int id);
        Task<List<Celular>> ListCelular();
        Task<int> UpdateCelular(Celular celular);
        Task<List<Celular>> FilterByStatus(string status);
        Task<List<Celular>> SearchByChamado(string chamado);
        Task<List<Celular>> SearchByPatrimonio(string patrimonio);
        Task<List<Celular>> GetByUnitAsync(string unit);
        Task AddAsync(Celular celular);
    }
}
