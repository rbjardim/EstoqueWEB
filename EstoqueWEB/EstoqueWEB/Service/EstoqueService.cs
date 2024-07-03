using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueWEB.Service
{
    namespace EstoqueWEB.Service
    {
        public class EstoqueService : IEstoqueService
        {
            private readonly IEstoqueRepository _estoqueRepository;
            private readonly UserManager<AplicationUser> _userManager;
            private readonly Context _dbContext;

            public EstoqueService(IEstoqueRepository estoqueRepository, UserManager<AplicationUser> userManager, Context dbContext)
            {
                _estoqueRepository = estoqueRepository;
                _userManager = userManager;
                _dbContext = dbContext;
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
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new List<Estoque>();
                }

                return await _estoqueRepository.GetEstoqueByUserId(user.Id);
            }

            public async Task<List<Estoque>> FilterByStatus(string status)
            {
                return await _estoqueRepository.FilterByStatus(status);
            }

            public async Task<List<Estoque>> SearchByChamadoOrPatrimonio(string chamado, string patrimonio)
            {
                return await _dbContext.Estoque
                    .Where(e => (string.IsNullOrEmpty(chamado) || e.Chamado.Contains(chamado)) ||
                                (string.IsNullOrEmpty(patrimonio) || e.Patrimonio.Contains(patrimonio)))
                    .ToListAsync();
            }
        }
    }
}
