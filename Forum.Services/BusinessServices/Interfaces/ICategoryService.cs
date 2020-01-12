using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Core.Common;
using Forum.Core.Model;

namespace Forum.Services.BusinessServices.Interfaces
{
    public interface ICategoryService
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        IEnumerable<Thread> GetAllThreadsByCategoryName(string name, PaginationRequest request);
        void AddCategory(Category category);
        void DeleteCategory(int id);
        void DeleteCategory(string categoryName, int id);
    }
}
