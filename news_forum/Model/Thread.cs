using news_forum.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using news_forum.Extensions;

namespace news_forum.Model
{
    public class Thread
    {
        #region Properties
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public string LatinName { get; set; }
        #endregion

        #region Associations

        public UserAccount UserAccount { get; set; }
        public Category Category { get; set; }
        public Status Status { get; set; }
        #endregion

        #region Constructor
        //EF Constructor
        protected Thread() { }

        public Thread(UserAccount userAccount, Category category, string subject, string description)
        {
            UserAccount = userAccount;
            Category = category;
            Subject = subject;
            Description = description;
            LatinName = subject.CyrilicToLatin();
            Created = DateTime.Now;
        }
        #endregion
    }
}
