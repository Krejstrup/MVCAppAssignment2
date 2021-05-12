using Microsoft.EntityFrameworkCore;
using MVCAppAssignment2.Models.Data;

namespace MVCAppAssignment2.Models.Repo
{
    public class PeopleDbContext : DbContext
    {

        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        { }


        // Config and Using Fluent API:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonLanguage>().HasKey(pl => new { pl.PersonId, pl.LanguageId });
        }

        public DbSet<Person> Peoples { get; set; }  // This is the entry we will use to use the database (as in #2)

        public DbSet<City> Cities { get; set; }     // New Table for the Cities

        public DbSet<Country> Countries { get; set; }     // New Table for the Countries

        public DbSet<Language> Languages { get; set; }     // New Table for the Languages

        //Jointable:
        public DbSet<PersonLanguage> PersonLanguages { get; set; }

    }
}