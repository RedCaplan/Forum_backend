using System;
using System.Linq;
using Forum.Core.Interfaces;
using Forum.DAL.EF.Context;
using Forum.DAL.Models.Entities;
using Forum.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Forum.DAL.EF
{
    public class DataInitializer : IDataInitizalizer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserAccount> _userManager;
        private UserAccount _testUserAccount;

        public DataInitializer(ApplicationDbContext context, UserManager<UserAccount> um)
        {
            _context = context;
            _userManager = um;
        }

        public void Seed()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            AddCategories();
            if (_userManager != null)
            {
                AddUsers();
                AddThreads();
                AddPosts();
                AddGroupCategory();
                AddUserGroup();
            }
        }

        private void AddUsers()
        {
            _userManager.CreateAsync(new UserAccount()
            {
                UserName = "test",
                Email = "test@gmail.com",
                BirthDay = DateTime.Now.AddYears(-20),
                Created = DateTime.Now,
                UserStatus = UserStatus.VERIFIED,
                IsModerator = true,
            }, "testpassword").Wait();

            _context.SaveChanges();

            _testUserAccount = _userManager.FindByEmailAsync("test@gmail.com").Result;
        }

        private void AddCategories()
        {
            var parentCategory = new Category("Community", "Community");
            _context.Categories.Add(parentCategory);
            _context.SaveChanges();

            var categories = new Category[]
            {
                new Category("General Discuss", "General discussion about the ...", parentCategory.Id),
                new Category("Feedback", "Tell us how to improve Forum!",parentCategory.Id)
            };

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
            var userGroup = new UserGroup(_testUserAccount, group);

            group.UserGroups.Add(userGroup);
            _context.SaveChanges();
        }
    }
}
