using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.Domain;

namespace OMS.Dal.EntityConfiguration
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(x => x.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(15);

        }
    }
}
