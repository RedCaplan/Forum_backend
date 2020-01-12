using System.Collections.Generic;
using Forum.Core.Model;

namespace Forum.Data.Repository.Interfaces
{
    public interface IGroupRepository
    {
        #region Interface Methods

        ICollection<Group> GetAllGroups();

        Group GetGroup(int id);

        void RemoveGroup(Group group);

        void AddGroup(Group group);

        void SaveChanges();

        #endregion
    }
}
