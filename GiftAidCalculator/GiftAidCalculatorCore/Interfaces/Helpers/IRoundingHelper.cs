namespace GiftAidCalculatorCore.Interfaces.Helpers
{
    public interface IRoundingHelper
    {
        /// <summary>
        /// Rounds the specified value to 2 decimal places.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        decimal Round2(decimal value);
    }
}
