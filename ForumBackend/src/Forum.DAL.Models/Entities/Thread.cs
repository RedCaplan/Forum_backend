using System;
using System.ComponentModel.DataAnnotations;
using Forum.Core.Extensions;
using Forum.DAL.Models.Enums;

namespace Forum.DAL.Models.Entities
{
    public class Thread : BaseEntity
    {
        #region Properties

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
            LatinName = subject.CyrillicToLatin();
            Created = DateTime.Now;
        }

        #endregion
    }
}
