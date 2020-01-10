using System.Collections.Generic;

namespace Forum.Model.Interfaces
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
