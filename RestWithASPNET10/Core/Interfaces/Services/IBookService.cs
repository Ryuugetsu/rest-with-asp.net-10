namespace Core
{   
    public interface IBookService
    {
        List<Book> FindAll();
        Book? FindById(int id);
        List<Book> FindWithPagedSearch(int page, int perPage ); 
        Book Create(Book person);
        Book? Update(Book person);
        void Delete(int id);
    }
}
