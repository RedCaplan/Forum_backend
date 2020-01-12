using System;
using System.Collections.Generic;
using System.Text;
using Forum.Services.BusinessServices;
using Xunit;

namespace Forum.UnitTests.Web.Services.CategoryServiceTests
{
    public class AddCategory
    {
        [Fact]
        public void ThrowsGivenNullCategory()
        {
            CategoryService categoryService = new CategoryService(null, null);

            Assert.Throws<ArgumentNullException>(() => categoryService.AddCategory(null));
        }
    }
}
