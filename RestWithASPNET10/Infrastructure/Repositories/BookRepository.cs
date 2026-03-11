using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book? FindById(int id)
        {
            return _context.Books.FirstOrDefault(p => p.Id == id);
        }

        public Book Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book? Update(Book book)
        {
            var result = _context.Books.AsNoTracking().SingleOrDefault(p => p.Id == book.Id);

            if (result == null)
                return null;

            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }

        public void Delete(int id)
        {
            var original = _context.Books.SingleOrDefault(p => p.Id == id);

            if (original == null)
                return;

            _context.Books.Remove(original);
            _context.SaveChanges();
        }
    }
}
