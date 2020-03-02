using System;
using Forum.Services.BusinessServices;
using Xunit;

namespace Forum.UnitTests.Web.Services.CategoryServiceTests
{
    public class GetCategoryByName
    {
        [Fact]
        public void ThrowsGivenNullName()
        {
            CategoryService categoryService = new CategoryService(null, null);

            Assert.Throws<ArgumentNullException>(() => categoryService.GetCategoryByName(null));
        }
    }
}
