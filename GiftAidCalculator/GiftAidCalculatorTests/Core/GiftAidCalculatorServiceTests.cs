using GiftAidCalculatorCore.Services;
using System.Threading.Tasks;
using Xunit;

namespace GiftAidCalculatorTests.Core
{
    public class GiftAidCalculatorServiceTests
    {
        private readonly GiftAidCalculatorService _giftAidCalculatorService;

        public GiftAidCalculatorServiceTests()
        {
            _giftAidCalculatorService = new GiftAidCalculatorService();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 25)]
        public async Task Calculate_ShouldReturnCorrectValue(
            decimal donation,
            decimal expected)
        {
            // Arrange

            // Act
            decimal actual = await _giftAidCalculatorService.Calculate(donation);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
