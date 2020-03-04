using System.Collections.Generic;
using Forum.DAL.Models.Entities;

namespace Forum.DAL.Repository.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        #region Interface Methods

        IEnumerable<Category> GetAllWithSubCategories();

        Category GetCategoryByName(string name);

        #endregion
    }
}
