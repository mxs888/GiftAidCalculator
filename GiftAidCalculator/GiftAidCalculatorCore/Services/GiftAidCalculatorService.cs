using GiftAidCalculatorCore.Interfaces.Database;
using GiftAidCalculatorCore.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace GiftAidCalculatorCore.Services
{
    public class GiftAidCalculatorService : IGiftAidCalculatorService
    {
        private readonly ITaxRateRepository _taxRateRepository;

        public GiftAidCalculatorService(ITaxRateRepository taxRateRepository)
        {
            _taxRateRepository = taxRateRepository;
        }

        public async Task<decimal> Calculate(decimal donation)
        {
            decimal taxRate = await _taxRateRepository.GetCurrentTaxRate();
            if (taxRate >= 100m)
            {
                throw new InvalidOperationException($"Invalid tax rate value {taxRate}");
            }

            return donation * (taxRate / (100m - taxRate));
        }
    }
}
