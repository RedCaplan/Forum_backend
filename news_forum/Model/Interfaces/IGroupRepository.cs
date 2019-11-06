using System.Collections.Generic;

namespace news_forum.Model.Interfaces
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
