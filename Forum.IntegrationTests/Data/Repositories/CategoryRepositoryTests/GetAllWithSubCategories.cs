using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forum.Core.Model;
using Forum.Data;
using Forum.Data.Repository;
using Forum.Data.Repository.Interfaces;
using Forum.UnitTests.Builders;
using Xunit;

namespace Forum.IntegrationTests.Data.Repositories.CategoryRepositoryTests
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

            Assert.Equal(_categoryRepository.GetAllWithSubCategories().First().SubCategories.Count, 1);
            Assert.Equal(_categoryRepository.GetAllWithSubCategories().First().Name, categoryParent.Name);
        }
    }
}
