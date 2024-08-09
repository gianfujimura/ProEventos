using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistance.Interface;
using ProEventos.Persistance.Contexts;

namespace ProEventos.Persistance
{
    public class PalestrantePersistance : IPalestrantePersistance
    {
        private readonly ProEventosContext _context;
        public PalestrantePersistance(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(p => p.RedesSociais);

            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(p => p.RedesSociais);

            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                .OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                .Include(p => p.RedesSociais);

            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query
                .Where(p => p.Id == PalestranteId)
                .OrderBy(p => p.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}