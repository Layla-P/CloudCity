using System.Threading.Tasks;
using CloudCityCakesMVC.Models.Entities;
using CloudCityCakesMVC.Models.Enums;
using CloudCityCakesMVC.Models.Helpers;

namespace CloudCityCakesMVC.Services.Rules
{
    public interface IStatusNotificationRule
    {
        Task<ServiceResponse> Notify(CakeOrder cakeOrder);
        bool Status(OrderStatus status);
    }
}