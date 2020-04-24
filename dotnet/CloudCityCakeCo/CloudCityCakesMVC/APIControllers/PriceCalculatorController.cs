using System;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudCityCakesMVC.APIControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceCalculatorController : ControllerBase
    {
        private readonly IPriceCalculationService _priceCalculationService;

        public PriceCalculatorController(IPriceCalculationService priceCalculationService)
        {
            _priceCalculationService = priceCalculationService ?? throw new ArgumentNullException(nameof(priceCalculationService));
        } 
        
        
        [HttpGet]
        public async Task<JsonResult> Get(string size, string flavour, string topping, string frosting)
        {
            var cake = new Cake
            {
                Size = size,
                Frosting = frosting,
                Topping = topping,
                Flavour = flavour
            };

            cake =  await _priceCalculationService.Total(cake);
            
            return new JsonResult(cake);
        }
        
    }
}