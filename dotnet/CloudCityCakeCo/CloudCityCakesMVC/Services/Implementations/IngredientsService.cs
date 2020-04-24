using System;
using System.Threading.Tasks;
using CloudCityCakesMVC.Data.Repositories;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Services.Interfaces;

namespace CloudCityCakesMVC.Services.Implementations
{
    
    public class IngredientsService : IIngredientsService
    {
        private readonly IIngredientsRepository _ingredientsRepository;

        public IngredientsService(IIngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository ?? throw new ArgumentNullException(nameof(ingredientsRepository));
            
        }
        
        public async Task<IngredientList> GetIngredientListAsync()
        {
            var ingredients = new IngredientList
            {
                Flavour = await _ingredientsRepository.GetFlavours(),
                Frosting = await _ingredientsRepository.GetFrostings(),
                Topping = await _ingredientsRepository.GetToppings()
            };
            
            return ingredients;
        }
    }
}