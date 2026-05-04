using Core;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class PersonRepository(DataContext context) : GenericRepository<Person>(context), IPersonRepository
    {
        public Person Disable(int id)
        {
            var person = _context.Set<Person>().FirstOrDefault(p => p.Id == id);
            if (person == null) return null;

            person.Enabled = false;
            //_context.Set<Person>().Update(person);
            _context.SaveChanges();
            return person; 
        }
    }
}
