namespace Core
{
    public interface IPersonRepository
    {
        List<Person> FindAll();
        Person? FindById(int id);
        Person Create(Person person);
        Person Update(Person person);
        void Delete(int id);
    }
}