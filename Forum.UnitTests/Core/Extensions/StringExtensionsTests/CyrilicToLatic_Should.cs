using System;
using System.Collections.Generic;
using System.Text;
using Forum.Core.Extensions;
using Xunit;

namespace Forum.UnitTests.Core.Extensions.StringExtensionsTests
{
    public class CyrilicToLatic_Should
    {
        [Fact]
        public void TranslateProperly()
        {
            var result = StringExtensions.CyrilicToLatin("Проверка кириллицы");

            Assert.Equal("Proverka-kirillicy", result);
        }
    }
}
