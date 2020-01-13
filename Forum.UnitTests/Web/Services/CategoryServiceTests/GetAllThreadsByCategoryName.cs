using System;
using Forum.Core.Common;
using Forum.Services.BusinessServices;
using Xunit;

namespace Forum.UnitTests.Web.Services.CategoryServiceTests
{
    public class GetAllThreadsByCategoryName
    {
        [Fact]
        public void ThrowsGivenNullName()
        {
            CategoryService categoryService = new CategoryService(null, null);
            PaginationRequest request = new PaginationRequest { PageIndex = 0, PageSize = 10 };

            Assert.Throws<ArgumentNullException>(() => categoryService.GetAllThreadsByCategoryName(null,request));
        }

        [Fact]
        public void ThrowsGivenNullPaginationRequest()
        {
            CategoryService categoryService = new CategoryService(null, null);
            string name = "test";

            Assert.Throws<ArgumentNullException>(() => categoryService.GetAllThreadsByCategoryName(name, null));
        }
    }
}
