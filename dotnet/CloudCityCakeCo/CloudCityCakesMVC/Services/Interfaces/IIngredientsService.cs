using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;

namespace CloudCityCakesMVC.Services.Interfaces
{
    public interface IIngredientsService
    {
        Task<IngredientList> GetIngredientListAsync();
    }
}