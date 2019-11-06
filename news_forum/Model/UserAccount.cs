using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using news_forum.Model.EFClasses;
using news_forum.Model.Enums;

namespace news_forum.Model
{
    public class UserAccount : IdentityUser
    {
        #region Extended Properties
        
        public DateTime BirthDay { get; set; }
        public DateTime Created { get; set; }
        public byte[] Avatar { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsModerator { get; set; }

        #endregion

        #region Associations

        public UserStatus UserStatus { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Thread> Threads { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        #endregion

        //#region Constructor
        public UserAccount() : base()
        {
        }
        //#endregion
    }
}
