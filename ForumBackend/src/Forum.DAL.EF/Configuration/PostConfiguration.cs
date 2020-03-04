using Forum.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.DAL.EF.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            #region Mapping

            builder.ToTable("Post");

            //ID Configuration
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.Content).IsRequired().HasMaxLength(2000);

            builder.HasMany(p => p.ReplyPosts)
                .WithOne(p => p.ReplyPost)
                .HasForeignKey(p => p.ReplyPostID);

            builder.HasOne(p => p.Thread)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
