using Library.Entities;
using Library.Infrastructure.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Library.Persistence.EF
{
    public class EFDataContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder _)
        //{
        //    base.OnConfiguring(_);
        //    _.UseSqlServer("Server=.;Database=Library;Trusted_Connection=True;");
        //}
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Entrust> Entrusts { get; set; }


        public EFDataContext(string connectionString)
           : this(new DbContextOptionsBuilder<EFDataContext>().UseSqlite(connectionString).Options)
        {
        }

        private EFDataContext(DbContextOptions<EFDataContext> options)
            : this((DbContextOptions)options)
        {
        }

        protected EFDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAssemblyConfigurations(typeof(EFDataContext).Assembly);
        }

        public override ChangeTracker ChangeTracker
        {
            get
            {
                var tracker = base.ChangeTracker;
                tracker.LazyLoadingEnabled = false;
                tracker.AutoDetectChangesEnabled = true;
                tracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
                return tracker;
            }
        }


    }
}
