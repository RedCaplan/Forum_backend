using System.Collections.Generic;
using System.Linq;
using Forum.DAL.Models.Entities;
using Forum.DAL.Repository;
using Forum.DAL.Repository.Interfaces;
using Forum.UnitTests.Builders;
using Xunit;

namespace Forum.UnitTests.Data.Repositories.GenericRepositotyTests
{
    public class Insert : BaseEfRepoTestFixture
    {
        private readonly IGenericRepository<Category> _genericCategoryRepository;

        public Insert()
        {
            _dbContext = GetRepository();
            _genericCategoryRepository = new GenericRepository<Category>(_dbContext);
        }


        [Fact]
        public void InsertCorrectCountOfItems()
        {
            int count = 5;
            List<Category> categories = new List<Category>(Enumerable.Range(0, count)
                .Select(x => new CategoryBuilder().WithDefaultValues().Build())
                .ToList());

            categories.ForEach(category => _genericCategoryRepository.Insert(category, true));
            var data = _genericCategoryRepository.GetAll();

            Assert.Equal(5, data.Count());
        }

    }
}
