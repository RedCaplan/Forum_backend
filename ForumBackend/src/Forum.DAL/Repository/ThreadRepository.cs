using System.Collections.Generic;
using System.Linq;
using Forum.Core.Common;
using Forum.DAL.EF.Context;
using Forum.DAL.Models.Entities;
using Forum.DAL.Models.Enums;
using Forum.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.DAL.Repository
{
    public class ThreadRepository : GenericRepository<Thread>, IThreadRepository
    {
        #region Fields

        private readonly DbSet<Thread> _threads;

        #endregion

        #region Constructor

        public ThreadRepository(ApplicationDbContext context) : base(context)
        {
            _threads = context.Threads;
        }

        #endregion

        #region Interface Methods

        public IEnumerable<Thread> GetAllWithIncludes()
        {
            return _threads
                .Include(t => t.UserAccount)
                .Include(t => t.Category);
        }

        public IEnumerable<Thread> GetThreadsByUser(UserAccount userAccount)
        {
            return _threads.Where(t => t.UserAccount.Id == userAccount.Id);
        }

        public IEnumerable<Thread> GetThreadsByCategory(Category category)
        {
            return _threads.Where(t => t.Category == category)
                .Include(t => t.UserAccount)
                .Include(t => t.Category);
        }

        public IEnumerable<Thread> GetThreadsByCategory(Category category, PaginationRequest request)
        {
            return GetThreadsByCategory(category)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize);
        }

        public IEnumerable<Thread> GetThreadsByStatus(Status status)
        {
            return _threads.Where(t => t.Status == status);
        }

        #endregion
    }
}
