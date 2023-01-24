using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Dal.EntityConfiguration
{
    public class DiscountConfig : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(x => x.DiscountType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasCheckConstraint("DiscountValue", "DiscountValue BETWEEN 0 AND 1");

            builder.HasData(new { Id = 1, DiscountType = "Discount 10+boxes", DiscountValue = 0.10m },
                            new { Id = 2, DiscountType = "Discount Starter Kit E-DaVinci", DiscountValue = 0.20m },
                            new { Id = 3, DiscountType = "Discount Starter Kit E-Leo", DiscountValue = 0.20m },
                            new { Id = 4, DiscountType = "Loyalty Discount", DiscountValue = 0.05m });

        }
    }
}
