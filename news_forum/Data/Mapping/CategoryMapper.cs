using Forum.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.Mapping
{
    public class CategoryMapper : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Mapping

            builder.ToTable("Category");

            //ID Configuration
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).IsRequired().ValueGeneratedOnAdd();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryID);

            #endregion
        }
    }
}
