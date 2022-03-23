using GiftAidCalculatorCore.Interfaces.Database;
using System.Threading.Tasks;

namespace GiftAidCalculatorInfrastructure.Database
{
    public class TaxRateRepository : ITaxRateRepository
    {
        public Task<decimal> GetCurrentTaxRate()
        {
            return Task.FromResult(20m);
        }
    }
}
