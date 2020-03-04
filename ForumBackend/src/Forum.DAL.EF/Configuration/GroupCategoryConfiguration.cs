using Forum.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.DAL.EF.Configuration
{
    public class GroupCategoryConfiguration : IEntityTypeConfiguration<GroupCategory>
    {
        public void Configure(EntityTypeBuilder<GroupCategory> builder)
        {
            #region Mapping

            builder.ToTable("Group_Category");
            builder.HasKey(gc => new { gc.GroupID, gc.CategoryID });

            builder.HasOne(gc => gc.Group)
                .WithMany(g => g.GroupCategories)
                .HasForeignKey(gc => gc.GroupID)
                .HasPrincipalKey(g => g.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gc => gc.Category)
                .WithMany()
                .HasForeignKey(gc => gc.CategoryID)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
