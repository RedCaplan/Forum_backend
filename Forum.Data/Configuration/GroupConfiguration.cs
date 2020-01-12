using Forum.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            #region Mapping

            builder.ToTable("Group");

            //ID Configuration
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(g => g.Name).IsRequired().HasMaxLength(100);

            #endregion
        }
    }
}
