namespace Core
{   
    public interface IBookService
    {
        List<Book> FindAll();
        Book? FindById(int id);
        Book Create(Book person);
        Book? Update(Book person);
        void Delete(int id);
    }
}
