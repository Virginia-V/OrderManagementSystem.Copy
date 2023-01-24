using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Dal.EntityConfiguration
{
    public class OrderedProductConfig : IEntityTypeConfiguration<OrderedProduct>
    {
        public void Configure(EntityTypeBuilder<OrderedProduct> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.ProductId });
        }
    }
}
