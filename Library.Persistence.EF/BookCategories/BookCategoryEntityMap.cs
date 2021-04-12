using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.BookCategories
{
    public class BookCategoryEntityMap : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> _)
        {
            _.ToTable("BookCategories");

            _.HasKey(_ => _.Id);
            _.Property("Id").IsRequired().ValueGeneratedOnAdd();

            _.Property("Title").IsRequired().HasMaxLength(100);

            _.HasMany(_ => _.Books).WithOne(_ => _.BookCategory)
                .HasForeignKey(_ => _.BookCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
