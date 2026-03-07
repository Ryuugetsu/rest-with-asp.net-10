using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class PersonService : IPersonService
    {
        public List<Person> FindAll()
        {
            return new List<Person>
            {
                MockPerson(1),
                MockPerson(2),
                MockPerson(3),
                MockPerson(4),
                MockPerson(5)
            };
        }

        public Person FindById(int id)
        {
            return MockPerson(id);
        }
       
        public Person Create(Person person)
        {
            return person;
        }

        public Person Update(Person person)
        {
            return person;
        }

        public void Delete(int id)
        {
            // delete logic here
        }



        public Person MockPerson(int i)
        {
            var person = new Person
            {
                Id = i,
                Name = "name" + i,
                Adrees = "adrees" + i,
                Gender = "Male"
            };

            return person;
        }
    }
}
