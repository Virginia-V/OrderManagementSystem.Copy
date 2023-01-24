using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Dal.EntityConfiguration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(x => x.BillingAddress)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(x => x.ShippingAddress)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}