using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{ 
    public class SQLContext : DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> options) : base(options){}

        public DbSet<Person> Persons { get; set; }
    }
}
