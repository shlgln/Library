using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.Persons
{
    class PersonEntityMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> _)
        {
            _.ToTable("Persons");

            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

            _.Property(_ => _.FullName).IsRequired().HasMaxLength(200);

            _.Property(_ => _.Age).IsRequired();

            _.Property(_ => _.Address).IsRequired().HasMaxLength(400);

            _.HasMany(_ => _.Entrusts).WithOne(_ => _.Person)
                .HasForeignKey(_ => _.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
