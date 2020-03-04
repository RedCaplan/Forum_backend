using System.Collections.Generic;
using Forum.Core.Common;
using Forum.DAL.Models.Entities;
using Forum.DAL.Models.Enums;

namespace Forum.DAL.Repository.Interfaces
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
