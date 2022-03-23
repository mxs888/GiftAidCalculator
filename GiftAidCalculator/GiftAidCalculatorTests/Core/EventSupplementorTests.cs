using GiftAidCalculatorCore;
using GiftAidCalculatorCore.Interfaces.Services;
using GiftAidCalculatorCore.Services;
using System.Threading.Tasks;
using Xunit;

namespace GiftAidCalculatorTests.Core
{
    public class EventSupplementorTests
    {
        private readonly IEventSupplementor _eventSupplementor;

        public EventSupplementorTests()
        {
            _eventSupplementor = new EventSupplementor();
        }

        [Theory]
        [InlineData(EventTypeEnum.Running, 105)]
        [InlineData(EventTypeEnum.Swimming, 103)]
        public async Task Apply_ShouldReturnCorrectValue(
            EventTypeEnum eventType,
            decimal expected)
        {
            // Arrange

            // Act
            decimal actual = await _eventSupplementor.Apply(100m, eventType);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
