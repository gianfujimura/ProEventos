using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistance.Interface
{
    public interface IEventoPersistance
    {
        // EVENTOS
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false);
    }
}