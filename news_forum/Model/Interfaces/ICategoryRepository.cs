using System.Collections.Generic;

namespace Forum.Model.Interfaces
{
    public interface ICategoryRepository
    {
        #region Interface Methods

        ICollection<Category> GetAllCategories();

        Category GetCategory(int id);

        Category GetCategoryByNameId(string name, int id);

        void RemoveCategory(Category category);

        void AddCategory(Category category);

        void SaveChanges();

        #endregion
    }
}
