using System;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.Helpers;
using CloudCityCakesMVC.Models.Entities;

namespace CloudCityCakesMVC.Services.Interfaces
{
    public interface INotificationHandler
    {
        Task<ServiceResponse> SendNotification(int orderId);
    }
}