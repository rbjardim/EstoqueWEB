using EstoqueWEB.Interface.Repository;
using EstoqueWEB.Model;
using EstoqueWEB.MySqlContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueWEB.Repository
{
    public class DevolucaoRepository : IDevolucaoRepository
    {
        private readonly Context _context;

        public DevolucaoRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> UpdateDevolucaoAsync(Devolucao devolucao)
        {
            _context.Entry(devolucao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return devolucao.Id;
        }

        public async Task<Devolucao> CreateDevolucao(Devolucao devolucao)
        {
            var ret = await _context.Devolucao.AddAsync(devolucao);
            await _context.SaveChangesAsync();
            ret.State = EntityState.Detached;
            return ret.Entity;
        }

        public async Task<bool> DeleteDevolucaoAsync(int Id)
        {
            var item = await _context.Devolucao.FindAsync(Id);
            if (item == null)
                return false;

            _context.Devolucao.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Devolucao>> ListDevolucao()
        {
            return await _context.Devolucao.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Devolucao> GetDevolucaoById(int id)
        {
            return await _context.Devolucao.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Devolucao>> FilterByStatus(string status)
        {
            return await _context.Devolucao.Where(e => e.Status == status).ToListAsync();
        }

        public async Task<int> UpdateDevolucao(Devolucao devolucao)
        {
            _context.Entry(devolucao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return devolucao.Id;
        }


        public Task<Estoque> GetDevolucaoByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Estoque>> GetDevolucaoByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        Task<Devolucao> IDevolucaoRepository.GetDevolucaoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<Devolucao>> IDevolucaoRepository.GetDevolucaoByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
