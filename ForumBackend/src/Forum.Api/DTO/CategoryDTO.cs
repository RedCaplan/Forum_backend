using System.Collections.Generic;

namespace Forum.Api.DTO
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
