using System.Collections.Generic;
using Forum.Core.Common;
using Forum.Core.Model;
using Forum.Core.Model.Enums;

namespace Forum.Data.Repository.Interfaces
{
    public interface IThreadRepository : IGenericRepository<Thread>
    {
        #region Interface Methods

        IEnumerable<Thread> GetAllWithIncludes();

        IEnumerable<Thread> GetThreadsByUser(UserAccount userAccount);

        IEnumerable<Thread> GetThreadsByCategory(Category category);

        IEnumerable<Thread> GetThreadsByCategory(Category category, PaginationRequest request);

        IEnumerable<Thread> GetThreadsByStatus(Status status);

        #endregion
    }
}
