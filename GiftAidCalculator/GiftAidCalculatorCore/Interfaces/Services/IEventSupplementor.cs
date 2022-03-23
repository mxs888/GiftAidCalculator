using System.Threading.Tasks;

namespace GiftAidCalculatorCore.Interfaces.Services
{
    public interface IEventSupplementor
    {
        Task<decimal> Apply(decimal amount, EventTypeEnum eventTypeEnum);
    }
}
