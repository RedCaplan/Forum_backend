using System.Collections.Generic;
using System.Linq;
using Forum.Model;
using Forum.Model.Enums;
using Forum.Model.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        #region Fields

        private readonly DbSet<Post> _posts;
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public PostRepository(ApplicationDbContext context)
        {
            _posts = context.Posts;
            _context = context;
        }

        #endregion

        #region Interface Methods

        public IEnumerable<Post> GetPostsByUser(UserAccount userAccount)
        {
            return _posts.Where(p => p.UserAccount.Id == userAccount.Id).ToList();
        }

        public IEnumerable<Post> GetPostsByThread(Thread thread)
        {
            return _posts.Where(p => p.Thread.ID == thread.ID).ToList();
        }

        public void RemovePost(Post post)
        {
            post.Status = Status.DELETED;
            _posts.Update(post);
        }

        public void UpdatePost(Post post)
        {
            _posts.Update(post);
        }

        public void AddPost(Post post)
        {
            _posts.Add(post);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        #endregion
    }
}
