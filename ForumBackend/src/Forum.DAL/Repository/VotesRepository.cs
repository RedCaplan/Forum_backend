using System.Collections.Generic;
using System.Linq;
using Forum.DAL.EF.Context;
using Forum.DAL.Models.Entities;
using Forum.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.DAL.Repository
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
