using Library.Entities;
using Library.Infrastructure.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Library.Persistence.EF
{
    public class EFDataContext:DbContext
    {
       

        public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Entrust> Entrusts { get; set; }


    }
}
