﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Forum.Core.Model;
using Forum.Core.Model.Enums;

namespace Forum.Web.DTO
{
    public class CategoryDTO
    {
        #region Properties

        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LatinName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }
      
        [Required]
        public List<CategoryDTO> SubCategories { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int? ParentCategoryID { get; set; }

        #endregion

        #region Constructor

        public CategoryDTO(Category category)
        {
            ID = category.ID;
            Name = category.Name;
            LatinName = category.LatinName;
            Description = category.Description;
            Created = category.Created;
            SubCategories = category.SubCategories.Select(c=>new CategoryDTO(c)).ToList();
            Status = Enum.GetName(typeof(Status), category.Status);
            ParentCategoryID = category.ParentCategoryID;
        }

        #endregion
    }
}
