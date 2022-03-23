using GiftAidCalculatorCore.Interfaces.Helpers;

namespace GiftAidCalculatorCommon
{
    public class RoundingHelper : IRoundingHelper
    {
        public decimal Round2(decimal value)
        {
            return decimal.Round(value, 2);
        }
    }
}
