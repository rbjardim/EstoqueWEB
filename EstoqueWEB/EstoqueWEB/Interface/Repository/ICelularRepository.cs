using EstoqueWEB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Repository
{
    public interface ICelularRepository
    {
        Task<int> UpdateCelularAsync(Celular celular);
        Task<int> UpdateCelular(Celular celular);
        Task<bool> DeleteCelularAsync(int Id);
        Task<Celular> CreateCelular(Celular celular);
        Task<List<Celular>> ListCelular();
        Task<Celular> GetCelularById(int id);
        Task<Celular> GetCelularByIdAsync(int id);
        Task<List<Celular>> GetCelularByUserId(string userId);

        Task<List<Celular>> FilterByStatus(string status);
        Task<List<Celular>> GetByUnitAsync(string unit);
    }
}
