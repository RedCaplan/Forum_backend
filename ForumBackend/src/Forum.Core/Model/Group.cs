using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.Core.Model.EFClasses;

namespace Forum.Core.Model
{
    public class Group : BaseEntity
    {
        #region Properties

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        #endregion

        #region Associations

        public ICollection<GroupCategory> GroupCategories { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }

        #endregion

        #region Constructor

        //EF Constructor
        protected Group() { }

        public Group(string name)
        {
            Name = name;
            GroupCategories = new List<GroupCategory>();
            UserGroups = new List<UserGroup>();
        }

        #endregion
    }
}
