namespace Forum.DAL.Models.Entities
{
    public class Votes : BaseEntity
    {
        #region Properties

        public int Value { get; set; }

        #endregion

        #region Associations

        public UserAccount UserAccount { get; set; }
        public int? ThreadID { get; set; }
        public Thread Thread { get; set; }
        public int? PostID { get; set; }
        public Post Post { get; set; }

        #endregion

        #region Constructor

        //EF Constructor
        protected Votes() { }

        #endregion
    }
}
