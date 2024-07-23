using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using EstoqueWEB.Repository;
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
        public class CelularService : ICelularService
        {
            private readonly ICelularRepository _celularRepository;
            private readonly UserManager<AplicationUser> _userManager;
            private readonly Context _dbContext;

            public CelularService(ICelularRepository celularRepository, UserManager<AplicationUser> userManager, Context dbContext)
            {
                _celularRepository = celularRepository;
                _userManager = userManager;
                _dbContext = dbContext;
            }

            public async Task<bool> CreateCelular(Celular celular)
            {
                return await _celularRepository.CreateCelular(celular) != null;
            }

            public async Task<bool> DeleteCelularAsync(int Idlocal)
            {
                await _celularRepository.DeleteCelularAsync(Idlocal);
                return true;
            }

            public async Task<Celular> GetCelularById(int id)
            {
                return await _celularRepository.GetCelularById(id);
            }

            public async Task<List<Celular>> ListCelular()
            {
                return await _celularRepository.ListCelular();
            }

            public async Task<int> UpdateCelular(Celular celular)
            {
                return await _celularRepository.UpdateCelular(celular);
            }

            public async Task<List<Celular>> GetCelularByUserId(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new List<Celular>();
                }

                return await _celularRepository.GetCelularByUserId(user.Id);
            }

            public async Task<List<Celular>> FilterByStatus(string status)
            {
                return await _celularRepository.FilterByStatus(status);
            }

            public async Task<List<Celular>> SearchByChamado(string chamado)
            {
                return await _dbContext.Celular
                    .Where(d => (string.IsNullOrEmpty(chamado) || d.Chamado.Contains(chamado)))

                    .ToListAsync();
            }

            public async Task<List<Celular>> SearchByPatrimonio(string patrimonio)
            {
                return await _dbContext.Celular
                    .Where(d => (string.IsNullOrEmpty(patrimonio) || d.Patrimonio.Contains(patrimonio)))

                    .ToListAsync();
            }

            public Task AddAsync(Celular celular)
            {
                throw new NotImplementedException();
            }
            public async Task<List<Celular>> GetByUnitAsync(string unit)
            {
                return await _celularRepository.GetByUnitAsync(unit);
            }
        }
    }
}
