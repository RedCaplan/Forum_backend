using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_forum.Model;

namespace news_forum.Data.Mapping
{
    public class GroupMapper : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            #region Mapping

            builder.ToTable("Group");

            //ID Configuration
            builder.HasKey(g => g.ID);
            builder.Property(g => g.ID).IsRequired().ValueGeneratedOnAdd();

            builder.Property(g => g.Name).IsRequired().HasMaxLength(100);

            #endregion
        }
    }
}
