using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistance.Interface;
using ProEventos.Persistance.Contexts;

namespace ProEventos.Persistance
{
    public class EventoPersistance : IEventoPersistance
    {
        private readonly ProEventosContext _context;
        public EventoPersistance(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes )
                .Include(e => e.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes )
                .Include(e => e.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
                .OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes )
                .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query
                .Where(e => e.Id == EventoId)
                .OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}