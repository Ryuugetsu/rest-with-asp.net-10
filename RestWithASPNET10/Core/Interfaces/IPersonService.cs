using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person Update(Person person);
        Person FindById(int id);
        List<Person> FindAll();
        void Delete(int id);
    }
}
