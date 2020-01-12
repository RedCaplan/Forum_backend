using Forum.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.Configuration
{
    public class VotesConfiguration : IEntityTypeConfiguration<Votes>
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
