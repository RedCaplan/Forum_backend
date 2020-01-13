using Forum.Core.Model;

namespace Forum.UnitTests.Builders
{
    public class UserAccountBuilder
    {
        private UserAccount _userAccount;

        public string TestEmail => "test@gmail.com";

        public string TestUserName => "testuser";

        public string TestPassword => "testpassword";

        public UserAccountBuilder Email(string email)
        {
            _userAccount.Email = email;
            return this;
        }

        public UserAccountBuilder UserName(string userName)
        {
            _userAccount.UserName = userName;
            return this;
        }

        public UserAccountBuilder WithDefaultValues()
        {
            _userAccount = new UserAccount{Email =  TestEmail, UserName =  TestUserName};
            return this;
        }

        public UserAccount Build() => _userAccount;
    }
}
