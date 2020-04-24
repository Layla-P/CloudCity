using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudCityCakesMVC.Models.ViewModels
{
    public class OrdersViewModel
    {
        public IList<CakeOrderViewModel> CakeOrders { get; set; }
    }
}