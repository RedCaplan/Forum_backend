using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.DAL.Models.Enums;

namespace Forum.DAL.Models.Entities
{
    public class Post : BaseEntity
    {
        #region Properties

        [Required]
        [StringLength(2000)]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        #endregion

        #region Associations

        public UserAccount UserAccount { get; set; }
        public Thread Thread { get; set; }  
        public int? ReplyPostID { get; set; }
        public Post ReplyPost { get; set; }
        public ICollection<Post> ReplyPosts { get; set; }
        public Status Status { get; set; }

        #endregion

        #region Constructor

        //EF Constructor
        protected Post() { }

        public Post(string content, UserAccount userAccount, Thread thread, int? replyPostId = null)
        {
            Content = content;
            UserAccount = userAccount;
            Thread = thread;
            ReplyPostID = replyPostId;
        }

        #endregion
    }
}
