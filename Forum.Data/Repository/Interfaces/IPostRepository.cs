using System.Collections.Generic;
using Forum.Core.Model;

namespace Forum.Data.Repository.Interfaces
{
    public interface IPostRepository
    {
        #region Interface Methods

        IEnumerable<Post> GetPostsByUser(UserAccount userAccount);

        IEnumerable<Post> GetPostsByThread(Thread thread);

        void RemovePost(Post post);

        void UpdatePost(Post post);

        void AddPost(Post post);

        void SaveChanges();

        #endregion
    }
}
