namespace Core
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> FindAll();
        T? FindById(int id);
        List<T> FindWithPagedSearch(int page, int perPage);
        T Create(T obj);
        T? Update(T obj);
        void Delete(int id);

        bool Exists(int id);
    }
}
