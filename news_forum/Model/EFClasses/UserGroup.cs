namespace news_forum.Model.EFClasses
{
    public class UserGroup
    {
        #region Attributes
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
