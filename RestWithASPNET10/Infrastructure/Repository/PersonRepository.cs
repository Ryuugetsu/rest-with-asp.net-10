using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class PersonRepository : IPersonRepository
    {
        private readonly SQLContext _context;

        public PersonRepository(SQLContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person? FindById(int id)
        {
            return _context.Persons.FirstOrDefault(p => p.Id == id);
        }

        public Person Create(Person person)
        {
            Person original = person.Id > 0 ? _context.Persons.FirstOrDefault(e => e.Id == person.Id) : null;

            _context.Persons.Add(person);
            _context.SaveChanges();
            return person;
        }

        public Person Update(Person person)
        {
            var result = _context.Persons.AsNoTracking().SingleOrDefault(p => p.Id == person.Id);

            if (result == null)
                throw new Exception("Person not found.");

            _context.Persons.Update(person);
            _context.SaveChanges();
            return person;
        }

        public void Delete(int id)
        {
            var original = _context.Persons.SingleOrDefault(p => p.Id == id);

            if (original == null)
                throw new Exception("Person not found.");

            //- Somente para via de estudos, vou remover ao invés de setar status desativado
            _context.Persons.Remove(original);
            _context.SaveChanges();
        }
    }
}