using System.Collections.Generic;
using System.Linq;
using Forum.Core.Model;
using Forum.Core.Model.Enums;
using Forum.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repository
{
    public class ThreadRepository : IThreadRepository
    {
        #region Fields

        private readonly DbSet<Thread> _threads;
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public ThreadRepository(ApplicationDbContext context)
        {
            _threads = context.Threads;
            _context = context;
        }

        #endregion

        #region Interface Methods

        public IEnumerable<Thread> GetAllThreads()
        {
            return _threads
                .Include(t=> t.UserAccount)
                .Include(t=>t.Category)
                .Include(t=>t.Status)
                .ToList();
        }

        public IEnumerable<Thread> GetThreadsByUser(UserAccount userAccount)
        {
            return _threads.Where(t => t.UserAccount.Id == userAccount.Id).ToList();
        }

        public IEnumerable<Thread> GetThreadsByCategory(Category category)
        {
            return _threads.Where(t => t.Category == category).ToList();
        }

        public IEnumerable<Thread> GetThreadsByStatus(Status status)
        {
            return _threads.Where(t => t.Status == status).ToList();
        }

        public Thread GetThread(int id)
        {
            return _threads.FirstOrDefault(t => t.ID == id);
        }

        public void RemoveThread(Thread thread)
        {
            _threads.Remove(thread);
        }

        public void AddThread(Thread thread)
        {
            _threads.Add(thread);
        }

        public void UpdateThread(Thread thread)
        {
            _threads.Update(thread);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
