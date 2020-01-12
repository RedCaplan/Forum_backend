using System.Collections.Generic;
using System.Linq;
using Forum.Core.Model;
using Forum.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        #region Fields

        private readonly DbSet<Category> _categories;

        #endregion

        #region Constructor

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _categories = context.Categories;
        }

        #endregion

        #region Interface Methods

        public IEnumerable<Category> GetAllWithSubCategories()
        {
            return _categories
                .Include(c => c.SubCategories)
                .Where(c => c.ParentCategory == null);
        }

        public Category GetCategoryByName(string name)
        {
            return _categories.FirstOrDefault(c => c.LatinName == name.ToLower());
        }

        #endregion
    }
}
