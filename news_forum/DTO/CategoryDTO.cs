using System;
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

        public string Name { get; set; }

        public string LatinName { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public List<CategoryDTO> SubCategories { get; set; }

        #endregion
    }
}
