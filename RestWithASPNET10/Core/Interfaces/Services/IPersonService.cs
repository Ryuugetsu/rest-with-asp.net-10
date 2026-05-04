namespace Core
{
    public interface IPersonService
    {
        List<Person> FindAll();
        Person? FindById(int id);
        Person Create(Person person);
        Person? Update(Person person);
        void Delete(int id);
        Person Disable(int id);
    }
}
