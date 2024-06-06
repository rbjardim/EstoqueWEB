﻿using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Interface.Service;
using EstoqueWEB.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
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
            var local = new Estoque();

            var result = await _estoqueRepository.CreateEstoque(estoque);
            return true;
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
            var local = await _estoqueRepository.ListEstoque();

            return local;
        }

        public Task<int> UpdateEstoque(Estoque estoque)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateEstoque(Estoque estoque)
        {
            var local = new Estoque();

            return await _estoqueRepository.UpdateEstoque(estoque);
        }

        Task<Estoque> IEstoqueService.GetEstoqueById(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Estoque>> IEstoqueService.ListEstoque()
        {
            throw new NotImplementedException();
        }
    }
}