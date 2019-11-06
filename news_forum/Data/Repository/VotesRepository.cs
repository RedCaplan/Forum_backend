using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using news_forum.Model;
using news_forum.Model.Interfaces;

namespace news_forum.Data.Repository
{
    public class VotesRepository : IVotesRepository
    {
        #region Attributes
        private readonly DbSet<Votes> _votes;
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public VotesRepository(ApplicationDbContext context)
        {
            _votes = context.Votes;
            _context = context;
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

        public void AddVotes(Votes votes)
        {
            _votes.Add(votes);
        }

        public void UpdateVotes(Votes votes)
        {
            _votes.Update(votes);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        #endregion
    }
}
