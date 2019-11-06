﻿namespace news_forum.Model.EFClasses
{
    public class GroupCategory
    {
        #region Attributes
        public int GroupID { get; set; }
        public Group Group { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }


        #endregion

        #region Constructor
        //EF Constructor    
        protected GroupCategory() { }

        public GroupCategory(Group group, Category category)
        {
            Group = group;
            Category = category;
        }
        #endregion
    }
}
