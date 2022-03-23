using System.Threading.Tasks;

namespace GiftAidCalculatorCore.Interfaces.Database
{
    public interface ITaxRateRepository
    {
        Task<decimal> GetCurrentTaxRate();
    }
}
