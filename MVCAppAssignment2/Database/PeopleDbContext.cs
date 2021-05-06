using Microsoft.EntityFrameworkCore;
using MVCAppAssignment2.Models.Data;

namespace MVCAppAssignment2.Models.Repo
{
    public class PeopleDbContext : DbContext
    {

        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        { }

        public DbSet<Person> Peoples { get; set; }  // This is the entry we will use to use the database (as in #2)

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }
    }
}