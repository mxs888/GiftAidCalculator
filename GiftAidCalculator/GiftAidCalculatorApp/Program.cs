using GiftAidCalculatorCommon;
using GiftAidCalculatorCore;
using GiftAidCalculatorCore.Interfaces.Database;
using GiftAidCalculatorCore.Interfaces.Helpers;
using GiftAidCalculatorCore.Interfaces.Services;
using GiftAidCalculatorCore.Services;
using GiftAidCalculatorInfrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GiftAidCalculatorApp
{
    class Program
    {
        static async Task Main(string[] _)
        {
            Console.WriteLine("Enter amount:");

            string amountParam = Console.ReadLine();
            if (!decimal.TryParse(amountParam, out decimal amount))
            {
                Console.WriteLine($"Invalid amount argument {amountParam}.");
                return;
            }

            Console.WriteLine(
                $"Enter event type ({string.Join(", ", Enum.GetNames(typeof(EventTypeEnum)).Select(x => x.ToLowerInvariant()))})");

            string eventTypeParam = Console.ReadLine();
            if (!Enum.TryParse<EventTypeEnum>(eventTypeParam, true, out EventTypeEnum eventType))
            {
                Console.WriteLine($"Invalid event type argument {eventTypeParam}.");
                return;
            }

            decimal result = await GetGiftAidCalculatorService().Calculate(amount, eventType);

            Console.WriteLine($"Result: {result}");
        }

        static IGiftAidCalculatorService GetGiftAidCalculatorService()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IGiftAidCalculatorService, GiftAidCalculatorService>();
            serviceCollection.AddSingleton<IRoundingHelper, RoundingHelper>();
            serviceCollection.AddSingleton<IEventSupplementor, EventSupplementor>();
            serviceCollection.AddSingleton<ITaxRateRepository, TaxRateRepository>();
            return serviceCollection.BuildServiceProvider().GetService<IGiftAidCalculatorService>();
        }
    }
}
