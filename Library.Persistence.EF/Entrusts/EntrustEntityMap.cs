using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.Entrusts
{
    class EntrustEntityMap : IEntityTypeConfiguration<Entrust>
    {
        public void Configure(EntityTypeBuilder<Entrust> _)
        {
            _.ToTable("Entrusts");

            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id).IsRequired().ValueGeneratedOnAdd();

            _.Property(_ => _.BookId).IsRequired();

            _.Property(_ => _.PersonId).IsRequired();

            _.Property(_ => _.ReturnDate);
            
        }
    }
}
