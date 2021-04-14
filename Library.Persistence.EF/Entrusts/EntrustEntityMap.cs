using Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            _.Property(_ => _.MemberId).IsRequired();

            _.Property(_ => _.ReturnDate);
        }
    }
}
