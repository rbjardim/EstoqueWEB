using EstoqueWEB.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Interface.Service
{
    public interface ILocalizacaoService
    {
        Task<bool> CreateEstoque(Estoque estoque);
        Task<bool> DeleteLocalAsync(int Idlocal);
        Task<Estoque> GetLocalizacaoById(int id);
        Task<List<Estoque>> ListLocal();
        Task<int> UpdateEstoque(Estoque estoque);

    }
}