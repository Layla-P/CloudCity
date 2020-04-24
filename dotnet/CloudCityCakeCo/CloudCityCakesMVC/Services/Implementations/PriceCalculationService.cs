using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Services.Interfaces;

namespace CloudCityCakesMVC.Services.Implementations
{
    public class PriceCalculationService : IPriceCalculationService
    {
        
        public Task<Cake> Total(Cake cake)
        {
            var sizePrice = GetSizePrices()[cake.Size];
            var flavourPrice = GetFlavourPrices()[cake.Flavour];
            var toppingPrice = GetToppingsPrices()[cake.Topping];
            var frostingPrice = GetFrostingPrices()[cake.Frosting];

            decimal[] cakeArray = {sizePrice, flavourPrice, toppingPrice, frostingPrice};
            cake.Price = cakeArray.Sum();
            return Task.FromResult(cake);
        }
        
        private static Dictionary<string, decimal> GetSizePrices()
        {
            return new Dictionary<string, decimal>
            {
                {"small", 5.0m},
                {"medium", 10.0m},
                {"large", 1.0m}
            };
        }

        private static Dictionary<string, decimal> GetFlavourPrices()
        {
            return new Dictionary<string, decimal>
            {
                {"red velvet", 5.0m},
                {"chocolate", 1.5m},
                {"carrot", 3.0m},
                {"rainbow", 10m},
                {"vanilla", 0}
            };
        }

        private static Dictionary<string, decimal> GetFrostingPrices()
        {
            return new Dictionary<string, decimal>
            {
                {"cream cheese", 5.5m},
                {"chocolate", 0},
                {"vanilla", 3.5m},
                {"maple", 10.5m}
            };
        }

        private static Dictionary<string, decimal> GetToppingsPrices()
        {
            return new Dictionary<string, decimal>
            {
                {"no topping", 0},
                {"sprinkles", 3.5m},
                {"bacon", 5.5m},
                {"Happy Birthday", 3.5m},
                {"sugar carrots", 3}
            };
        }


    }
}