using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Dal.EntityConfiguration
{
    public class PaymentTermConfig : IEntityTypeConfiguration<PaymentTerm>
    {
        public void Configure(EntityTypeBuilder<PaymentTerm> builder)
        {
            builder.Property(x => x.PaymentTermsType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(new { Id = 1, PaymentTermsType = "Prepaid" },
                            new { Id = 2, PaymentTermsType = "Net 30" },
                            new { Id = 3, PaymentTermsType = "Net 60" },
                            new { Id = 4, PaymentTermsType = "Net 90" },
                            new { Id = 5, PaymentTermsType = "Net 120" });
        }
    }
}
