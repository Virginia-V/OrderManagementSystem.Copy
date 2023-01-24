using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Dal.EntityConfiguration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ProductSKU)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.ProductPrice)
                .IsRequired();
        }
    }
}
