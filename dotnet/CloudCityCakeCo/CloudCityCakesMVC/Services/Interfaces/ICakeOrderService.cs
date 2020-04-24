using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Models.Enums;
using CloudCityCakesMVC.Models.Helpers;
using CloudCityCakesMVC.Models.ViewModels;

namespace CloudCityCakesMVC.Services.Interfaces
{
    public interface ICakeOrderService
    {
        Task<ServiceResponse<CakeOrderViewModel>> GetCakeOrderByIdAsync(int id);
        Task<ServiceResponse<IList<CakeOrderViewModel>>> GetAllCakeOrdersAsync();
        Task<ServiceResponse<IList<CakeOrderViewModel>>> GetAllCakeOrdersAsync(OrderStatus status);
        Task<ServiceResponse<IList<CakeOrderViewModel>>> GetAllCakeOrdersByUserIdAsync(int userId);
        Task<ServiceResponse<CakeOrderViewModel>> AddNewOrderAsync(OrderDetails orderDetails);
        Task<ServiceResponse<CakeOrderViewModel>> UpdateOrderStatusAsync(CakeOrderViewModel vm);
    }
}