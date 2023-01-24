using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.Domain;

namespace OMS.Dal.EntityConfiguration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.PurchaseOrderNumber)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
