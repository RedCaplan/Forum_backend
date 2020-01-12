using System.Collections.Generic;
using System.Linq;
using Forum.Core.Model;
using Forum.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repository
{
    public class GroupRepository : IGroupRepository
    {
        #region Fields

        private readonly DbSet<Group> _groups;
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public GroupRepository(ApplicationDbContext context)
        {
            _groups = context.Groups;
            _context = context;
        }

        #endregion

        #region Interface Methods

        public ICollection<Group> GetAllGroups()
        {
            return _groups.ToList();
        }

        public Group GetGroup(int id)
        {
            return _groups.FirstOrDefault(g => g.ID == id);
        }

        public void RemoveGroup(Group group)
        {
            _groups.Remove(group);
        }

        public void AddGroup(Group group)
        {
            _groups.Add(group);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
