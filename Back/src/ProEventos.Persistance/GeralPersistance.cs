using System.Threading.Tasks;
using ProEventos.Persistance.Interface;
using ProEventos.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ProEventos.Persistance
{
    public class GeralPersistance : IGeralPersistance
    {
        private readonly ProEventosContext _context;
        public GeralPersistance(ProEventosContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}