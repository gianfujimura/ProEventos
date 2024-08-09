using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistance.Interface
{
    public interface IPalestrantePersistance
    {
        // PALESTRANTES
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false);
        Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos = false);
    }
}