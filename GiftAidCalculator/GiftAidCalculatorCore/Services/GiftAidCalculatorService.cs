using GiftAidCalculatorCore.Interfaces.Services;
using System.Threading.Tasks;

namespace GiftAidCalculatorCore.Services
{
    public class GiftAidCalculatorService : IGiftAidCalculatorService
    {
        private readonly decimal _taxRage = 20m;

        public GiftAidCalculatorService()
        {
        }

        public Task<decimal> Calculate(decimal donation)
        {
            return Task.FromResult(donation * (_taxRage / (100m - _taxRage)));
        }
    }
}
