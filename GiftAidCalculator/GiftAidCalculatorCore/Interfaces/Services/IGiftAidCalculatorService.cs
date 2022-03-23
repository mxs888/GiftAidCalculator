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
    }
}
