using Forum.Core.Model;
using Forum.Data.Repository;
using Forum.Data.Repository.Interfaces;
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
        public void GetsExistingCategory()
        {
            Category existingCategory = new CategoryBuilder()
                .WithDefaultValues()
                .Build();
            _categoryRepository.Insert(existingCategory, true);
            string categoryName = existingCategory.Name;

            Category categoryFromRepo = _categoryRepository.GetCategoryByName(categoryName);
            Assert.Equal(existingCategory.ParentCategoryID, categoryFromRepo.ParentCategoryID);

            var subCategories = categoryFromRepo.SubCategories;
            Assert.Equal(existingCategory.SubCategories, subCategories);
        }

    }
}
