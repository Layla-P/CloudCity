using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudCityCakesMVC.APIControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            _ingredientsService = ingredientsService ?? throw new ArgumentNullException(nameof(ingredientsService));
        }

        [HttpGet]
        public Task<JsonResult> Get()
        {
            
            var flavours = new IngredientList
            {
                Flavour = new List<string> {"red velvet", "chocolate", "vanilla", "carrot", "rainbow"},
                Frosting = new List<string> {"cream cheese", "chocolate", "vanilla", "maple"},
                Topping = new List<string> {"sprinkles", "bacon", "Happy Birthday", "sugar carrots"}
            };
            
            return Task.FromResult(new JsonResult(flavours));
        }
    }
}