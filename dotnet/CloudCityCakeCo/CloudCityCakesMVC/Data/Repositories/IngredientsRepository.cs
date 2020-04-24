using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudCityCakesMVC.Data.Repositories
{
    
    public class IngredientsRepository : IIngredientsRepository
    {
        public Task<List<string>> GetFlavours()
        {
            return Task.FromResult(new List<string> {"red velvet", "chocolate", "vanilla", "carrot", "rainbow"});
        }

        public Task<List<string>> GetToppings()
        {
            return Task.FromResult(new List<string> {"cream cheese", "chocolate", "vanilla", "maple"});
        }

        public Task<List<string>> GetFrostings()
        {
           return Task.FromResult(new List<string> {"sprinkles", "bacon", "Happy Birthday", "sugar carrots"});
        }
    }
}