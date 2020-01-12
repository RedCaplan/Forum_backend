using System.Collections.Generic;
using System.Linq;
using Forum.Core.Model;
using Forum.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repository
{
    public class VotesRepository : GenericRepository<Votes>, IVotesRepository
    {
        #region Fields

        private readonly DbSet<Votes> _votes;

        #endregion

        #region Constructor

        public VotesRepository(ApplicationDbContext context) : base(context)
        {
            _votes = context.Votes;
        }

        #endregion

        #region Interface Methods

        public IEnumerable<Votes> GetVotesByPost(int postId)
        {
            return _votes.Where(p => p.PostID == postId);
        }

        public IEnumerable<Votes> GetVotesByThread(int threadId)
        {
            return _votes.Where(p => p.ThreadID == threadId);
        }

        public bool VotesExists(UserAccount userAccount, int? postId, int? threadId)
        {
            return _votes.FirstOrDefault(v=> v.UserAccount == userAccount && (v.PostID == postId || v.ThreadID == threadId)) != null;
        }

        #endregion
    }
}
