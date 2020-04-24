using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CloudCityCakesMVC.APIControllers
{
    [ApiController]
    [Route("api/orders")]
    public class CakeOrderController : ControllerBase
    {

        // POST: api/orders
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]OrderDetails orderDetails)
        {
            return new JsonResult("success");
        }
    }
}


