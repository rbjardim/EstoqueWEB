using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueWEB.Repository
{
    public class CelularRepository : ICelularRepository
    {
        private readonly Context _context;

        public CelularRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> UpdateCelularAsync(Celular celular)
        {
            _context.Entry(celular).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return celular.Id;
        }

        public async Task<Celular> CreateCelular(Celular celular)
        {
            var ret = await _context.Celular.AddAsync(celular);
            await _context.SaveChangesAsync();
            ret.State = EntityState.Detached;
            return ret.Entity;
        }

        public async Task<bool> DeleteCelularAsync(int Id)
        {
            var item = await _context.Celular.FindAsync(Id);
            if (item == null)
                return false;

            _context.Celular.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Celular>> ListCelular()
        {
            return await _context.Celular.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Celular> GetCelularById(int id)
        {
            return await _context.Celular.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Celular>> FilterByStatus(string status)
        {
            return await _context.Celular.Where(e => e.Status == status).ToListAsync();
        }

        public async Task<int> UpdateCelular(Celular celular)
        {
            _context.Entry(celular).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return celular.Id;
        }


        public Task<Celular> GetCelularByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Celular>> GetCelularByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        Task<Celular> ICelularRepository.GetCelularByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Celular>> ICelularRepository.GetCelularByUserId(string userId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Celular>> GetByUnitAsync(string unit)
        {
            return await _context.Celular
                .Where(e => e.Chamado.StartsWith(unit))
                .ToListAsync();
        }

    }
}
