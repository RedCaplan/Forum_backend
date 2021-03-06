﻿using System.Linq;
using Forum.DAL.EF.Context;
using Forum.DAL.Models.Entities;
using Forum.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.DAL.Repository
{
    public class UserAccountRepository : IUserAccountRepository
    {
        #region Fields

        private readonly DbSet<UserAccount> _users;
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public UserAccountRepository(ApplicationDbContext context)
        {
            _users = context.UserAccounts;
            _context = context;
        }

        #endregion

        #region Interface Methods

        public void AddUser(UserAccount userAccount)
        {
            _users.Add(userAccount);
        }

        public void RemoveUser(UserAccount userAccount)
        {
            _users.Remove(userAccount);
        }

        public bool EmailExists(string email)
        {
            return _users.Select(u => u.Email).Contains(email);
        }

        public UserAccount GetUserByEmail(string email)
        {
            return _users
                .Include(u => u.UserStatus)
                .Include(u => u.Posts)
                .ThenInclude(p => p.Status)
                .Include(u => u.Threads)
                .ThenInclude(t => t.Category)
                .FirstOrDefault(u => u.Email == email);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
