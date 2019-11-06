﻿using news_forum.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using news_forum.Extensions;

namespace news_forum.Model
{
    public class Category
    {
        #region Properties
            public int ID { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [StringLength(1000)]
            public string Description { get; set; }

            public DateTime Created { get; set; }
            public string LatinName { get; set; }

        #endregion

        #region Associations
        public Status Status { get; set; }
           public int? ParentCategoryID { get; set; }
           public Category ParentCategory { get; set; }
           public ICollection<Category> SubCategories { get; set; }
        #endregion

        #region Constructor
        //EF Constructor
        protected Category()
        {
            SubCategories = new HashSet<Category>();
        }

        public Category(string name, string description, Category parentCategory = null, Status status = Status.APPROVED): this()
        {
            Name = name;
            Description = description;
            LatinName = name.CyrilicToLatin();
            Status = status;
            ParentCategory = parentCategory;
            Created = DateTime.Now;
        }
        #endregion



    }
}
    