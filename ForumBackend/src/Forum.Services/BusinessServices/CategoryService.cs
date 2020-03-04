using System.Collections.Generic;
using Ardalis.GuardClauses;
using Forum.Core.Common;
using Forum.DAL.Models.Entities;
using Forum.DAL.Repository.Interfaces;
using Forum.Services.BusinessServices.Interfaces;

namespace Forum.Services.BusinessServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IThreadRepository _threadRepository;

        public CategoryService(ICategoryRepository categoryRepo, IThreadRepository threadRepo)
        {
            _categoryRepository = categoryRepo;
            _threadRepository = threadRepo;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetAllWithSubCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category GetCategoryByName(string name)
        {
            Guard.Against.NullOrEmpty(name,nameof(name));

            return _categoryRepository.GetCategoryByName(name);
        }

        public IEnumerable<Thread> GetAllThreadsByCategoryName(string name, PaginationRequest request)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.Null(request, nameof(request));

            return _threadRepository.GetThreadsByCategory(GetCategoryByName(name), request);
        }

        public void AddCategory(Category category)
        {
            Guard.Against.Null(category, nameof(category));

            _categoryRepository.Insert(category, true);
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id,true);
        }

        public void DeleteCategory(string categoryName)
        {
            Guard.Against.NullOrEmpty(categoryName, nameof(categoryName));

            Category category = _categoryRepository.GetCategoryByName(categoryName);
            _categoryRepository.Delete(category);
        }
    }
}
