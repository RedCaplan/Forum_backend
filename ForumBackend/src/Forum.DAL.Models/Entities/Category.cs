﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.Core.Extensions;
using Forum.DAL.Models.Enums;

namespace Forum.DAL.Models.Entities
{
    public class Category : BaseEntity
    {
        #region Properties

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

        public Category(string name, string description, int? parentCategoryID = null, Status status = Status.APPROVED) : this()
        {
            Name = name;
            Description = description;
            LatinName = name.CyrillicToLatin();
            Status = status;
            ParentCategoryID = parentCategoryID;
            Created = DateTime.Now;
        }

        #endregion



    }
}
