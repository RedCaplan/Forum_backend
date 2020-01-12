using Forum.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Data.Configuration
{
    public class ThreadConfiguration : IEntityTypeConfiguration<Thread>
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
