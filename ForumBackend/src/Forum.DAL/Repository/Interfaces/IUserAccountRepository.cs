using Forum.Core.Model;

namespace Forum.Data.Repository.Interfaces
{
    public interface IUserAccountRepository
    {
        #region Interface Methods

        void AddUser(UserAccount userAccount);

        void RemoveUser(UserAccount userAccount);

        bool EmailExists(string email);

        UserAccount GetUserByEmail(string email);

        void SaveChanges();

        #endregion
    }
}
