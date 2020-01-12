using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Forum.Core.Common;
using Forum.Core.Model;
using Forum.Data.Repository.Interfaces;
using Forum.Services.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Forum.Services.BusinessServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IThreadRepository _threadRepository;
        private readonly UserManager<UserAccount> _userManager;

        public CategoryService(ICategoryRepository categoryRepo, IThreadRepository threadRepo, UserManager<UserAccount> um)
        {
            _categoryRepository = categoryRepo;
            _threadRepository = threadRepo;
            _userManager = um;
        }

        public ICollection<Category> GetCategories()
        {
            return _categoryRepository.GetAllWithSubCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category GetCategoryByName(string name)
        {
            return _categoryRepository.GetCategoryByName(name);
        }

        public IEnumerable<Thread> GetAllThreadsByCategoryName(string name, PaginationRequest request)
        {
            return _threadRepository.GetThreadsByCategory(GetCategoryByName(name), request);
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.Insert(category, true);
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id,true);
        }

        public void DeleteCategory(string categoryName, int id)
        {
            throw new NotImplementedException();
        }
    }
}
