﻿using System;
using System.Collections.Generic;
using Forum.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace Forum.DAL.Models.Entities
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

        #region Constructor

        public UserAccount() : base()
        {
            UserGroups = new List<UserGroup>();
            Threads = new List<Thread>();
            Posts = new List<Post>();
        }

        #endregion
    }
}
