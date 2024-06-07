using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueWEB.Service
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly UserManager<AplicationUser> _userManager;

        public EstoqueService(IEstoqueRepository estoqueRepository, UserManager<AplicationUser> userManager)
        {
            _estoqueRepository = estoqueRepository;
            _userManager = userManager;
        }

        public async Task<bool> CreateEstoque(Estoque estoque)
        {
            return await _estoqueRepository.CreateEstoque(estoque) != null;
        }


        public async Task<bool> DeleteEstoqueAsync(int Idlocal)
        {
            await _estoqueRepository.DeleteEstoqueAsync(Idlocal);
            return true;
        }

        public async Task<Estoque> GetEstoqueById(int id)
        {
            return await _estoqueRepository.GetEstoqueById(id);
        }

        public async Task<List<Estoque>> ListEstoque()
        {
            return await _estoqueRepository.ListEstoque();
        }

        public async Task<int> UpdateEstoque(Estoque estoque)
        {
            return await _estoqueRepository.UpdateEstoque(estoque);
        }


        public async Task<List<Estoque>> GetEstoqueByUserId(string userId)
        {
           
            return new List<Estoque>();
        }
    }
}
