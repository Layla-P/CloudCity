using System;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Services.Interfaces;
using CloudCityCakesMVC.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudCityCakesMVC.APIControllers
{
    [ApiController]
    [Route("api/orders")]
    public class CakeOrderController : ControllerBase
    {
        private readonly ICakeOrderService _cakeOrderService;
        public CakeOrderController(ICakeOrderService cakeOrderService)
        {
            _cakeOrderService = cakeOrderService ?? throw new ArgumentNullException(nameof(cakeOrderService));
           
        }

        // POST: api/orders
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]OrderDetails orderDetails)
        {
           var order = await _cakeOrderService.AddNewOrderAsync(orderDetails);
            return new JsonResult(order.Content.Id.ToString());
        }
    }
}


