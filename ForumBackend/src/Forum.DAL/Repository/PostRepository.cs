using System.Collections.Generic;
using System.Linq;
using Forum.DAL.EF.Context;
using Forum.DAL.Models.Entities;
using Forum.DAL.Models.Enums;
using Forum.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.DAL.Repository
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        #region Fields

        private readonly DbSet<Post> _posts;

        #endregion

        #region Constructor

        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _posts = context.Posts;
        }

        #endregion

        #region Interface Methods

        public IEnumerable<Post> GetPostsByUser(UserAccount userAccount)
        {
            return _posts.Where(p => p.UserAccount.Id == userAccount.Id).ToList();
        }

        public IEnumerable<Post> GetPostsByThread(Thread thread)
        {
            return _posts.Where(p => p.Thread.Id == thread.Id).ToList();
        }

        public void SoftDeletePost(Post post)
        {
            post.Status = Status.DELETED;
            Update(post, true);
        }

        #endregion
    }
}
