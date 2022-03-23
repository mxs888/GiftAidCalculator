using GiftAidCalculatorCore;
using GiftAidCalculatorCore.Interfaces.Database;
using GiftAidCalculatorCore.Interfaces.Helpers;
using GiftAidCalculatorCore.Interfaces.Services;
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
        private readonly Mock<IRoundingHelper> _roundingHelperMock;
        private readonly Mock<IEventSupplementor> _eventSupplementMock;

        private readonly GiftAidCalculatorService _giftAidCalculatorService;

        public GiftAidCalculatorServiceTests()
        {
            _taxRateRepositoryMock = new Mock<ITaxRateRepository>();
            _roundingHelperMock = new Mock<IRoundingHelper>();
            _eventSupplementMock = new Mock<IEventSupplementor>();

            _taxRateRepositoryMock.Setup(m => m.GetCurrentTaxRate())
                .ReturnsAsync(20m);

            _roundingHelperMock.Setup(m => m.Round2(It.IsAny<decimal>()))
                .Returns<decimal>(value => value);

            _giftAidCalculatorService = new GiftAidCalculatorService(
                _taxRateRepositoryMock.Object,
                _roundingHelperMock.Object,
                _eventSupplementMock.Object);
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
            _roundingHelperMock.Verify(m => m.Round2(expected), Times.Once);
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
            _roundingHelperMock.Verify(m => m.Round2(expected), Times.Once);
        }

        [Fact]
        public async Task Calculate_ShouldReturnCorrectValueWithEventType()
        {
            // Arrange
            _eventSupplementMock.Setup(m => m.Apply(It.IsAny<decimal>(), EventTypeEnum.Running))
                .ReturnsAsync(200m);

            // Act
            decimal actual = await _giftAidCalculatorService.Calculate(100m, EventTypeEnum.Running);

            // Assert
            Assert.Equal(200m, actual);
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
            _roundingHelperMock.Verify(m => m.Round2(It.IsAny<decimal>()), Times.Never);
        }
    }
}
