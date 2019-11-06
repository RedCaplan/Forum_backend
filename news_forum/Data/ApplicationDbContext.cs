using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using news_forum.Data.Mapping;
using news_forum.Model;
using news_forum.Model.EFClasses;

namespace news_forum.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserAccount>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Votes> Votes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration<Group>(new GroupMapper())
                .ApplyConfiguration<Category>(new CategoryMapper())
                .ApplyConfiguration<UserGroup>(new UserGroupMapper())
                .ApplyConfiguration<GroupCategory>(new GroupCategoryMapper())
                .ApplyConfiguration<Post>(new PostMapper())
                .ApplyConfiguration<Thread>(new ThreadMapper())
                .ApplyConfiguration<Votes>(new VotesMapper());
        }
    }
}
