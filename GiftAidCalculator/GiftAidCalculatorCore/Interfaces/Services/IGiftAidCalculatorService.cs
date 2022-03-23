using System.Threading.Tasks;

namespace GiftAidCalculatorCore.Interfaces.Services
{
    public interface IGiftAidCalculatorService
    {
        /// <summary>
        /// Calculates a gift aid amount based on the donation provided.
        /// </summary>
        /// <param name="donation"></param>
        /// <returns></returns>
        Task<decimal> Calculate(decimal donation);

        /// <summary>
        /// Calculates a gift aid amount based on the donation provided and an event type.
        /// </summary>
        /// <param name="donation"></param>
        /// <param name="eventTypeEnum"></param>
        /// <returns></returns>
        Task<decimal> Calculate(decimal donation, EventTypeEnum eventTypeEnum);
    }
}
