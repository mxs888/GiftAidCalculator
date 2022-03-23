using GiftAidCalculatorCore.Interfaces.Database;
using GiftAidCalculatorCore.Interfaces.Helpers;
using GiftAidCalculatorCore.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace GiftAidCalculatorCore.Services
{
    public class GiftAidCalculatorService : IGiftAidCalculatorService
    {
        private readonly ITaxRateRepository _taxRateRepository;
        private readonly IRoundingHelper _roundingHelper;

        public GiftAidCalculatorService(
            ITaxRateRepository taxRateRepository,
            IRoundingHelper roundingHelper)
        {
            _taxRateRepository = taxRateRepository;
            _roundingHelper = roundingHelper;
        }

        public async Task<decimal> Calculate(decimal donation)
        {
            decimal taxRate = await _taxRateRepository.GetCurrentTaxRate();
            if (taxRate >= 100m)
            {
                throw new InvalidOperationException($"Invalid tax rate value {taxRate}");
            }

            return _roundingHelper.Round2(donation * (taxRate / (100m - taxRate)));
        }
    }
}
