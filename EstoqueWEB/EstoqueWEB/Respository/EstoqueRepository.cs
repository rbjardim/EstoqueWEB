using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using EstoqueWEB.MySqlContext;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> UpdateEstoque(Estoque user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<Estoque> GetEstoqueById(int id)
        {
            return await _context.Estoque.FirstOrDefaultAsync(l => l.Id == id);
        }

        public Task<Estoque> CreateNotebook(Estoque notebook)
        {
            throw new NotImplementedException();
        }

        public Task<Estoque> GetEstoqueById(int id)
        {
            throw new NotImplementedException();
        }
    }
}