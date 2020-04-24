using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.Enums;
using CloudCityCakesMVC.Models.ViewModels;
using CloudCityCakesMVC.Services.Interfaces;
using CloudCityCakesMVC.APIControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CloudCityCakesMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICakeOrderService _cakeOrderService;

        public AdminController(ICakeOrderService cakeOrderService)
        {
            _cakeOrderService = cakeOrderService ?? throw new ArgumentNullException(nameof(cakeOrderService));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders()
        {
            var orders = await _cakeOrderService.GetAllCakeOrdersAsync();
            var vm = new OrdersViewModel
            {
                CakeOrders = orders.Content
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var vm = await _cakeOrderService.GetCakeOrderByIdAsync(id);
            
            vm.Content.Statuses = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int) v).ToString(),
                Selected = v==vm.Content.OrderStatus
            }).ToList();
            
            return View(vm.Content);
        }


        [HttpPost]
        public async Task<IActionResult> Update(CakeOrderViewModel model)
        {
            var order = await _cakeOrderService.UpdateOrderStatusAsync(model);
            return RedirectToAction("Orders");
        }
    }
}