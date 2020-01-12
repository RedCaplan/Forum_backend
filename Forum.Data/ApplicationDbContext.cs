using Forum.Core.Model;
using Forum.Core.Model.EFClasses;
using Forum.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserAccount>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Votes> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration<Group>(new GroupConfiguration())
                .ApplyConfiguration<Category>(new CategoryConfiguration())
                .ApplyConfiguration<UserGroup>(new UserGroupConfiguration())
                .ApplyConfiguration<GroupCategory>(new GroupCategoryConfiguration())
                .ApplyConfiguration<Post>(new PostConfiguration())
                .ApplyConfiguration<Thread>(new ThreadConfiguration())
                .ApplyConfiguration<Votes>(new VotesConfiguration());
        }
    }
}
