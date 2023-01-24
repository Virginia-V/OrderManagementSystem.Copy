using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Dal.EntityConfiguration
{
    public class PaymentStatusConfig : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(new { Id = 1, Status = "Paid" }, new { Id = 2, Status = "UnPaid" });
        }
    }
}
