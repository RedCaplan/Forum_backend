using System.Collections.Generic;
using Forum.Core.Model;

namespace Forum.Data.Repository.Interfaces
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
