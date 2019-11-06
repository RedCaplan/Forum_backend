using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_forum.Model;
using NJsonSchema.Infrastructure;

namespace news_forum.Data.Mapping
{
    public class ThreadMapper : IEntityTypeConfiguration<Thread>
    {
        public void Configure(EntityTypeBuilder<Thread> builder)
        {
            #region Mapping
            builder.ToTable("Thread");

            //ID Configuration
            builder.HasKey(t => t.ID);
            builder.Property(t => t.ID).IsRequired().ValueGeneratedOnAdd();

            builder.Property(t => t.Subject).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Description).IsRequired().HasMaxLength(1000);

            builder.HasOne(t => t.Category)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
