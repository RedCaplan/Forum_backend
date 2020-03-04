using Forum.DAL.Models.Entities;
using Forum.DAL.Repository;
using Forum.DAL.Repository.Interfaces;
using Forum.UnitTests.Builders;
using Xunit;

namespace Forum.UnitTests.Data.Repositories.CategoryRepositoryTests
{
    public class GetCategoryByName : BaseEfRepoTestFixture
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByName()
        {
            _dbContext = GetRepository();
            _categoryRepository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public void GetsExistingCategory_WithCorrectId()
        {
            Category existingCategory = new CategoryBuilder()
                .WithDefaultValues()
                .Build();
            _categoryRepository.Insert(existingCategory, true);
            string categoryName = existingCategory.Name;

            Category categoryFromRepo = _categoryRepository.GetCategoryByName(categoryName);
            Assert.Equal(existingCategory.ParentCategoryID, categoryFromRepo.ParentCategoryID);
        }

        [Fact]
        public void GetsExistingCategory_WithCorrectSubCategories()
        {
            Category existingCategory = new CategoryBuilder()
                .WithDefaultValues()
                .Build();
            _categoryRepository.Insert(existingCategory, true);
            string categoryName = existingCategory.Name;

            Category categoryFromRepo = _categoryRepository.GetCategoryByName(categoryName);
            var subCategories = categoryFromRepo.SubCategories;

            Assert.Equal(existingCategory.SubCategories, subCategories);
        }

    }
}
