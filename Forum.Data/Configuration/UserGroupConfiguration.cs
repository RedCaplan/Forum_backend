using Forum.Core.Model.EFClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.Configuration
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
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
                .HasPrincipalKey(g => g.Id)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
