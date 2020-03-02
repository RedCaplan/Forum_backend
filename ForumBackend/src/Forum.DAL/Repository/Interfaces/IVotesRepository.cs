using System.Collections.Generic;
using Forum.Core.Model;

namespace Forum.Data.Repository.Interfaces
{
    public interface IVotesRepository : IGenericRepository<Votes>
    {
        #region Interface Methods     

        IEnumerable<Votes> GetVotesByPost(int postId);

        IEnumerable<Votes> GetVotesByThread(int threadId);

        bool VotesExists(UserAccount userAccount, int? postId, int? threadId);

        #endregion
    }
}
