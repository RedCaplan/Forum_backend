using System.Collections.Generic;
using System.Linq;
using Forum.Core.Model;
using Forum.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Fields

        private readonly DbSet<Category> _categories;
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public CategoryRepository(ApplicationDbContext context)
        {
            _categories = context.Categories;
            _context = context;
        }

        #endregion

        #region Interface Methods

        public ICollection<Category> GetAllCategories()
        {
            return _categories
                .Include(c=>c.SubCategories)
                .Where(c=>c.ParentCategory==null)
                .ToList();
        }

        public Category GetCategory(int id)
        {
            return _categories.FirstOrDefault(c => c.ID == id);
        }

        public Category GetCategoryByNameId(string name, int id)
        {
            return _categories.FirstOrDefault(c => c.LatinName == name.ToLower() && c.ID == id);
        }

        public void RemoveCategory(Category category)
        {
            _categories.Remove(category);
        }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}
