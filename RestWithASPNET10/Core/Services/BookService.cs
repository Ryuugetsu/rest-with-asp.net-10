namespace Core
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> FindAll()
        {
            return _bookRepository.FindAll();
        }

        public Book? FindById(int id)
        {
            return _bookRepository.FindById(id);
        }

        public Book Create(Book person)
        {
            return _bookRepository.Create(person);
        }

        public Book? Update(Book person)
        {
            return _bookRepository.Update(person);
        }

        public void Delete(int id)
        {
            _bookRepository.Delete(id);
        }
    }
}
