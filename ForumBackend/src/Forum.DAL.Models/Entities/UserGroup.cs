namespace Forum.DAL.Models.Entities
{
    public class UserGroup
    {
        #region Properties

        public string UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }

        public int GroupID { get; set; }
        public Group Group { get; set; }

        #endregion

        #region Constructor

        //EF Constructor
        protected UserGroup() { }

        public UserGroup(UserAccount userAccount, Group group)
        {
            UserAccount = userAccount;
            Group = group;
        }

        #endregion
    }
}
