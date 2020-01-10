using System.Collections.Generic;
using Forum.Model.Enums;

namespace Forum.Model.Interfaces
{
    public interface IThreadRepository
    {
        #region Interface Methods

        IEnumerable<Thread> GetAllThreads();

        IEnumerable<Thread> GetThreadsByUser(UserAccount userAccount);

        IEnumerable<Thread> GetThreadsByCategory(Category category);

        IEnumerable<Thread> GetThreadsByStatus(Status status);

        Thread GetThread(int id);

        void RemoveThread(Thread thread);

        void AddThread(Thread thread);

        void UpdateThread(Thread thread);

        void SaveChanges();

        #endregion
    }
}
