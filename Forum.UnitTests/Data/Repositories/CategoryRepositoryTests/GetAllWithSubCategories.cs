using System.Linq;
using Forum.Core.Model;
using Forum.Data.Repository;
using Forum.Data.Repository.Interfaces;
using Forum.UnitTests.Builders;
using Xunit;

namespace Forum.UnitTests.Data.Repositories.CategoryRepositoryTests
{
    public class GetAllWithSubCategories : BaseEfRepoTestFixture
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllWithSubCategories()
        {
            _dbContext = GetRepository();
            _categoryRepository = new CategoryRepository(_dbContext);
        }

        [Fact]
        public void GetIncludesData()
        {
            Category categoryParent = new CategoryBuilder()
                .WithDefaultValues()
                .Build();

            _categoryRepository.Insert(categoryParent, true);

            Category categoryParentRepo = _categoryRepository.GetCategoryByName(categoryParent.Name);

            Category subCategory = new CategoryBuilder()
                .ParentId(categoryParentRepo.Id)
                .Name("Test subCategory")
                .Description("Test Description")
                .Build();

            _categoryRepository.Insert(subCategory, true);

            Assert.Equal(1, _categoryRepository.GetAllWithSubCategories().First().SubCategories.Count);
            Assert.Equal(categoryParent.Name, _categoryRepository.GetAllWithSubCategories().First().Name);
        }
    }
}
