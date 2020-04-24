using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudCityCakesMVC.Data.Repositories
{
    public interface IIngredientsRepository
    {
        Task<List<string>> GetFlavours();
        Task<List<string>> GetToppings();
        Task<List<string>> GetFrostings();
    }
}