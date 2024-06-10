using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueWEB.Repository
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly Context _context;

        public EstoqueRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> UpdateEstoqueAsync(Estoque estoque)
        {
            _context.Entry(estoque).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return estoque.Id;
        }

        public async Task<Estoque> CreateEstoque(Estoque estoque)
        {
            var ret = await _context.Estoque.AddAsync(estoque);
            await _context.SaveChangesAsync();
            ret.State = EntityState.Detached;
            return ret.Entity;
        }

        public async Task<bool> DeleteEstoqueAsync(int Id)
        {
            var item = await _context.Estoque.FindAsync(Id);
            if (item == null)
                return false;

            _context.Estoque.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Estoque>> ListEstoque()
        {
            List<Estoque> list = await _context.Estoque.OrderBy(p => p.Id).ToListAsync();
            return list;
        }

        public async Task<Estoque> GetEstoqueById(int id)
        {
            return await _context.Estoque.FirstOrDefaultAsync(l => l.Id == id);
        }

        public Task<int> UpdateEstoque(Estoque estoque)
        {
            throw new NotImplementedException();
        }

        public Task<Estoque> GetEstoqueByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Estoque>> GetEstoqueByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
