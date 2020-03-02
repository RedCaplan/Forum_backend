using System.Collections.Generic;
using Forum.Core.Model;

namespace Forum.Data.Repository.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        #region Interface Methods

        IEnumerable<Category> GetAllWithSubCategories();

        Category GetCategoryByName(string name);

        #endregion
    }
}
