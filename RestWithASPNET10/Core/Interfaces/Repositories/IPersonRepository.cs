using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(int id);
    }
}
