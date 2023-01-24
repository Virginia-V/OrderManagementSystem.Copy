using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.Domain;

namespace OMS.Dal.EntityConfiguration
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CategoryName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(new { Id = 1, CategoryName = "Machine" },
                            new { Id = 2, CategoryName = "Cover" },
                            new { Id = 3, CategoryName = "Document" },
                            new { Id = 4, CategoryName = "Replacement" });
        }
    }
}
