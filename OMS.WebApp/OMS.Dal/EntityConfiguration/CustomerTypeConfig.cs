using OMS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OMS.Dal.EntityConfiguration
{
    public class CustomerTypeConfig : IEntityTypeConfiguration<CustomerType>
    {
        public void Configure(EntityTypeBuilder<CustomerType> builder)
        {
            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("CustomerType");


            builder.HasData(new { Id = 1, Type = "Library" },
                            new { Id = 2, Type = "Outreach" },
                            new { Id = 3, Type = "Private School" },
                            new { Id = 4, Type = "Public School" },
                            new { Id = 5, Type = "Referral" },
                            new { Id = 6, Type = "University" },
                            new { Id = 7, Type = "Trade Show" },
                            new { Id = 8, Type = "Agent" },
                            new { Id = 9, Type = "Inbound Lead:Distributor" },
                            new { Id = 10, Type = "Inbound Lead:Facebook" },
                            new { Id = 11, Type = "Inbound Lead:Google" },
                            new { Id = 12, Type = "Dealer - Demo" },
                            new { Id = 13, Type = "Parochial School" },
                            new { Id = 14, Type = "Technical College" },
                            new { Id = 15, Type = "Bookseller" });
        }
    }
}
