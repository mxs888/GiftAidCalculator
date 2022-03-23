using GiftAidCalculatorCore.Interfaces.Services;
using System.Threading.Tasks;

namespace GiftAidCalculatorCore.Services
{
    public class EventSupplementor : IEventSupplementor
    {
        public Task<decimal> Apply(decimal amount, EventTypeEnum eventTypeEnum)
        {
            decimal supplement = eventTypeEnum switch
            {
                EventTypeEnum.Running => 0.05m,
                EventTypeEnum.Swimming => 0.03m,
                _ => 0m
            };

            return Task.FromResult((amount * supplement) + amount);
        }
    }
}
