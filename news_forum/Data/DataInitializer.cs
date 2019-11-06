using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using news_forum.DTO;
using news_forum.Model;
using news_forum.Model.EFClasses;
using news_forum.Model.Enums;

namespace news_forum.Data
{
    public class DataInitializer
    {
        private ApplicationDbContext _context { get; set; }
        private readonly UserManager<UserAccount> _um;
        private IConfiguration _config;
        private UserAccount _testUserAccount;

        public DataInitializer(ApplicationDbContext context, UserManager<UserAccount> um, IConfiguration config)
        {
            _context = context;
            _um = um;
            _config = config;
        }

        public void Seed()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            AddUsers();
            AddCategories();
            AddThreads();
            AddPosts();
            AddGroupCategory();
            AddUserGroup();

        }

        private void AddUsers()
        {
            RegisterDTO dto = new RegisterDTO()
            {
                Email = "test@gmail.com",
                BirthDay = DateTime.Now.AddYears(-20),
                Password = "testpassword",
                Username = "test"
            };

            _um.CreateAsync(new UserAccount()
            {
                UserName = dto.Username,
                Email = dto.Email,
                BirthDay = dto.BirthDay,
                Created = DateTime.Now,
                UserStatus = UserStatus.VERIFIED,
                IsModerator = true,
            }, dto.Password).Wait();

            _context.SaveChanges();

            _testUserAccount = _um.FindByEmailAsync("test@gmail.com").Result;
        }

        private void AddCategories()
        {
            var parentCategory = new Category("Community", "Community");
            var categories = new Category[]
            {
                new Category("General Discuss", "General discussion about the ...", parentCategory),
                new Category("Feedback", "Tell us how to improve Forum!",parentCategory)
            };


            _context.Categories.Add(parentCategory);
            _context.Categories.AddRange(categories);
            _context.SaveChanges();
        }

        private void AddThreads()
        {
            var categories = _context.Categories.Where(c => c.ParentCategory != null).ToList();

            foreach (var category in categories)
            {
                var threads = new Thread[]
                {
                    new Thread(_testUserAccount,category,"New Thread #1", "Testing threads"),
                    new Thread(_testUserAccount,category,"New Thread #2", "Testing threads"),
                };
                _context.Threads.AddRange(threads);
            }
            _context.SaveChanges();
        }

        private void AddPosts()
        {
            var threads = _context.Threads.ToList();
            foreach (var thread in threads)
            {
                var post = new Post("Test post message", _testUserAccount, thread);

                _context.Posts.Add(post);
            }

            _context.SaveChanges();
        }

        private void AddGroupCategory()
        {
            var group = new Group("Moderator");
            var categories = _context.Categories.ToList();

         
            foreach (var category in categories)
            {
                var group_category = new GroupCategory(group, category);
                group.GroupCategories.Add(group_category);
                _context.Groups.Add(group);
            }

            _context.SaveChanges();
        }

        private void AddUserGroup()
        {
            var group = _context.Groups.FirstOrDefault();
            var user_group = new UserGroup(_testUserAccount, group);

            group.UserGroups.Add(user_group);
            _context.SaveChanges();
        }

    }
}
