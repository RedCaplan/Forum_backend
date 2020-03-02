using System;
using Forum.Core.Model;
using Forum.Data.Repository.Interfaces;
using Forum.Services.BusinessServices;
using Forum.UnitTests.Builders;
using Moq;
using Xunit;

namespace Forum.UnitTests.Web.Services.CategoryServiceTests
{
    public class DeleteCategory
    {
        private Mock<ICategoryRepository> _mockCategoryRepo;

        public DeleteCategory()
        {
            _mockCategoryRepo = new Mock<ICategoryRepository>();
        }

        [Fact]
        public void Should_InvokeCategoryRepositoryDelete_Once()
        {
            Category category = new CategoryBuilder()
                .WithDefaultValues()
                .Build();

            _mockCategoryRepo.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(category);

            CategoryService categoryService = new CategoryService(_mockCategoryRepo.Object, null);

            categoryService.DeleteCategory(It.IsAny<int>());

            _mockCategoryRepo.Verify(x => x.Delete(It.IsAny<int>(), true), Times.Once);
        }

        [Fact]
        public void ThrowsGivenNullName()
        {
            CategoryService categoryService = new CategoryService(null, null);

            Assert.Throws<ArgumentNullException>(() => categoryService.DeleteCategory(null));
        }

    }
}
