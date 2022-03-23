using GiftAidCalculatorCore.Interfaces.Database;
using GiftAidCalculatorCore.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GiftAidCalculatorTests.Core
{
    public class GiftAidCalculatorServiceTests
    {
        private readonly Mock<ITaxRateRepository> _taxRateRepositoryMock;

        private readonly GiftAidCalculatorService _giftAidCalculatorService;

        public GiftAidCalculatorServiceTests()
        {
            _taxRateRepositoryMock = new Mock<ITaxRateRepository>();
            _taxRateRepositoryMock.Setup(m => m.GetCurrentTaxRate())
                .ReturnsAsync(20m);

            _giftAidCalculatorService = new GiftAidCalculatorService(
                _taxRateRepositoryMock.Object);
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

        [Theory]
        [InlineData(100, 0, 0)]
        [InlineData(100, 50, 100)]
        [InlineData(100, 75, 300)]
        public async Task Calculate_ShouldReturnCorrectValueWhenDifferentTaxRate(
            decimal donation,
            decimal taxRate,
            decimal expected)
        {
            // Arrange
            _taxRateRepositoryMock.Setup(m => m.GetCurrentTaxRate())
                .ReturnsAsync(taxRate);

            // Act
            decimal actual = await _giftAidCalculatorService.Calculate(donation);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Calculate_ThrowExceptionIfTaxRateIsInvalid()
        {
            // Arrange
            _taxRateRepositoryMock.Setup(m => m.GetCurrentTaxRate())
                .ReturnsAsync(100m);

            // Act
            Task awaitable = _giftAidCalculatorService.Calculate(100);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await awaitable);
        }
    }
}
