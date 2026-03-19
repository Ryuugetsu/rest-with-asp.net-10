using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public List<T> FindAll()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> FindWithPagedSearch(int page, int perPage)
        {
            IQueryable<T> query= _context.Set<T>().AsNoTracking();
            query = query.OrderBy(e => e.Id);
            query = query.Skip((page - 1) * perPage).Take(perPage);

            return query.ToList();
        }

        public T? FindById(int id)
        {
            return _context.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public T Create(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public T? Update(T obj)
        {
            var result = _context.Set<T>().AsNoTracking().SingleOrDefault(e => e.Id == obj.Id);

            if (result == null) return null;

            _context.Set<T>().Update(obj);
            _context.SaveChanges();
            return obj;
        }

        public void Delete(int id)
        {
            var original = _context.Set<T>().SingleOrDefault(e => e.Id == id);
            if (original == null) return;

            //- The right is change status to deleted, not remove from database
            _context.Set<T>().Remove(original);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }
    }
}
