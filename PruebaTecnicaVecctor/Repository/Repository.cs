using Microsoft.EntityFrameworkCore;
using PruebaTecnicaVecctor.ContextoBBDD;
using PruebaTecnicaVecctor.Repository.Interfaz;

namespace PruebaTecnicaVecctor.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void AddRange(List<T> Entities)
        {
            dbSet.AddRange(Entities);
            _context.SaveChanges();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

    }
}
