using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_forum.Model.EFClasses;

namespace news_forum.Data.Mapping
{
    public class UserGroupMapper : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            #region Mapping
            builder.ToTable("User_Group");
            builder.HasKey(ug => new { ug.UserAccountId, ug.GroupID });

            builder.HasOne(ug => ug.UserAccount)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserAccountId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ug => ug.Group)
                .WithMany(g=>g.UserGroups)
                .HasForeignKey(ug => ug.GroupID)
                .HasPrincipalKey(g => g.ID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
