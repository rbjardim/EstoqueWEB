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
        public class DevolucaoService : IDevolucaoService
        {
            private readonly IDevolucaoRepository _devolucaoRepository;
            private readonly UserManager<AplicationUser> _userManager;
            private readonly Context _dbContext;

            public DevolucaoService(IDevolucaoRepository devolucaoRepository, UserManager<AplicationUser> userManager, Context dbContext)
            {
                _devolucaoRepository = devolucaoRepository;
                _userManager = userManager;
                _dbContext = dbContext;
            }

            public async Task<bool> CreateDevolucao(Devolucao devolucao)
            {
                return await _devolucaoRepository.CreateDevolucao(devolucao) != null;
            }

            public async Task<bool> DeleteEstoqueAsync(int Idlocal)
            {
                await _devolucaoRepository.DeleteDevolucaoAsync(Idlocal);
                return true;
            }

            public async Task<Devolucao> GetDevolucaoById(int id)
            {
                return await _devolucaoRepository.GetDevolucaoById(id);
            }

            public async Task<List<Devolucao>> ListDevolucao()
            {
                return await _devolucaoRepository.ListDevolucao();
            }

            public async Task<int> UpdateDevolucao(Devolucao devolucao)
            {
                return await _devolucaoRepository.UpdateDevolucao(devolucao);
            }

            public async Task<List<Devolucao>> GetDevolucaoByUserId(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new List<Devolucao>();
                }

                return await _devolucaoRepository.GetDevolucaoByUserId(user.Id);
            }

            public async Task<List<Devolucao>> FilterByStatus(string status)
            {
                return await _devolucaoRepository.FilterByStatus(status);
            }

            public async Task<List<Devolucao>> SearchByChamado(string chamado)
            {
                return await _dbContext.Devolucao
                    .Where(d => (string.IsNullOrEmpty(chamado) || d.Chamado.Contains(chamado)))

                    .ToListAsync();
            }

            public async Task<List<Devolucao>> SearchByPatrimonio(string patrimonio)
            {
                return await _dbContext.Devolucao
                    .Where(d => (string.IsNullOrEmpty(patrimonio) || d.Patrimonio.Contains(patrimonio)))

                    .ToListAsync();
            }

            public Task<bool> DeleteDevolucaoAsync(int id)
            {
                throw new NotImplementedException();
            }
        }
    }
}
