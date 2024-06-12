using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using Microsoft.EntityFrameworkCore;
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
            return await _context.Estoque.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Estoque> GetEstoqueById(int id)
        {
            return await _context.Estoque.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Estoque>> FilterByStatus(string status)
        {
            return await _context.Estoque.Where(e => e.Status == status).ToListAsync();
        }

        public async Task<int> UpdateEstoque(Estoque estoque)
        {
            _context.Entry(estoque).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return estoque.Id;
        }


        public Task<Estoque> GetEstoqueByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Estoque>> GetEstoqueByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

    }
}
