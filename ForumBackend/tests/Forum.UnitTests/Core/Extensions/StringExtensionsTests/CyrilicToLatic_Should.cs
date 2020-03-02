using Forum.Core.Extensions;
using Xunit;

namespace Forum.UnitTests.Core.Extensions.StringExtensionsTests
{
    public class CyrilicToLatic_Should
    {
        [Fact]
        public void TranslateProperly()
        {
            var result = StringExtensions.CyrillicToLatin("Проверка кириллицы");
            var expected = "Proverka-kirillicy";

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TranslateNonCyrillicTextProperly()
        {
            var result = StringExtensions.CyrillicToLatin("Vérification de texte");
            var expected = "Vérification-de-texte";

            Assert.Equal(expected, result);
        }
    }
}
