using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Dal.EntityConfiguration
{
    public class OrderTypeConfig : IEntityTypeConfiguration<OrderType>
    {
        public void Configure(EntityTypeBuilder<OrderType> builder)
        {
            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("OrderType");

            builder.HasData(new { Id = 1, Type = "New Order" }, new { Id = 2, Type = "ReOrder" });
        }
    }
}
