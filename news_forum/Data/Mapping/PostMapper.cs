﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using news_forum.Model;

namespace news_forum.Data.Mapping
{
    public class PostMapper : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            #region Mapping

            builder.ToTable("Post");

            //ID Configuration
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).IsRequired().ValueGeneratedOnAdd();

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
