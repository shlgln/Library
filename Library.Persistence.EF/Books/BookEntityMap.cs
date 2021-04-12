using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.EF.Books
{
    public class BookEntityMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> _)
        {
            _.ToTable("Books");

            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

            _.Property(_ => _.BookCategoryId).IsRequired();

            _.Property(_ => _.Title).IsRequired().HasMaxLength(200);

            _.Property(_ => _.Author).IsRequired().HasMaxLength(100);

            _.Property(_ => _.MinimumAge).IsRequired();

            _.HasMany(_ => _.Entrusts).WithOne(_ => _.Book)
                .HasForeignKey(_ => _.BookId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
