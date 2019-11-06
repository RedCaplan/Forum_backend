using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_forum.Model;

namespace news_forum.Data.Mapping
{
    public class VotesMapper : IEntityTypeConfiguration<Votes>
    {
        public void Configure(EntityTypeBuilder<Votes> builder)
        {
            #region Mapping
            builder.ToTable("Votes");

            //ID Configuration
            builder.HasKey(v => v.ID);
            builder.Property(v => v.ID).IsRequired().ValueGeneratedOnAdd();
            #endregion
        }
    }
}
