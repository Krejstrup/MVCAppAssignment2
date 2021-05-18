using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCAppAssignment2.Models.Data;

namespace MVCAppAssignment2.Models.Repo
{
    public class PeopleDbContext : IdentityDbContext<IdentityUser>  // this was ": DbContext" for just database
    {

        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options) //step 2
        { }


        // Config the join table  Using Fluent API:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-- Start up the Identity modelling first by override: ------Step 2.2--
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonLanguage>().HasKey(pl => new { pl.PersonId, pl.LanguageId });


            //---------- So what does the following code: --------------------------
            // I suppose that we really don't need this code, it works without it...

            modelBuilder.Entity<PersonLanguage>()
                .HasOne<Person>(pl => pl.Person)
                .WithMany(pe => pe.PersonLanguages)
                .HasForeignKey(pl => pl.PersonId);

            modelBuilder.Entity<PersonLanguage>()
                .HasOne<Language>(pl => pl.Language)
                .WithMany(pe => pe.PersonLanguages)
                .HasForeignKey(pl => pl.LanguageId);
            //-----------------------------------------------------------------------
        }

        public DbSet<Person> Peoples { get; set; }  // This is the entry we will use to use the database (as in #2)

        public DbSet<City> Cities { get; set; }     // New Table for the Cities

        public DbSet<Country> Countries { get; set; }     // New Table for the Countries

        public DbSet<Language> Languages { get; set; }     // New Table for the Languages

        //Jointable:
        public DbSet<PersonLanguage> PersonLanguages { get; set; }

    }
}