using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_forum.Model.EFClasses;

namespace news_forum.Data.Mapping
{
    public class GroupCategoryMapper : IEntityTypeConfiguration<GroupCategory>
    {
        public void Configure(EntityTypeBuilder<GroupCategory> builder)
        {
            #region Mapping
            builder.ToTable("Group_Category");
            builder.HasKey(gc => new { gc.GroupID, gc.CategoryID });

            builder.HasOne(gc => gc.Group)
                .WithMany(g => g.GroupCategories)
                .HasForeignKey(gc => gc.GroupID)
                .HasPrincipalKey(g => g.ID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gc => gc.Category)
                .WithMany()
                .HasForeignKey(gc => gc.CategoryID)
                .HasPrincipalKey(c => c.ID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
