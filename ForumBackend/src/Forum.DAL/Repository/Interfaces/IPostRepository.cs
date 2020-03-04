using System.Collections.Generic;
using Forum.DAL.Models.Entities;

namespace Forum.DAL.Repository.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        #region Interface Methods

        IEnumerable<Post> GetPostsByUser(UserAccount userAccount);

        IEnumerable<Post> GetPostsByThread(Thread thread);

        void SoftDeletePost(Post post);

        #endregion
    }
}
