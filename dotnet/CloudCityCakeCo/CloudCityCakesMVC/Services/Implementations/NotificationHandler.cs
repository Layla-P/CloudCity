using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCityCakesMVC.Data.Repositories;
using CloudCityCakesMVC.Models.Helpers;
using CloudCityCakesMVC.Services.Interfaces;
using CloudCityCakesMVC.Services.Rules;
using CloudCityCakesMVC.Models.Entities;

namespace CloudCityCakesMVC.Services.Implementations
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly List<IStatusNotificationRule> _notificationRules;
        private readonly ICakeOrderRepository _cakeRepository;
        
        public NotificationHandler(ICakeOrderRepository cakeRepository, IEnumerable<IStatusNotificationRule> notificationRules)
        {
            _cakeRepository = cakeRepository ?? throw new ArgumentNullException(nameof(cakeRepository));
            _notificationRules = notificationRules.ToList();
        }
        
        public async Task<ServiceResponse> SendNotification(int orderId)
        {
            var serviceResponse = new ServiceResponse();
            
            var cakeOrder = await _cakeRepository.GetById(orderId);
            
            IStatusNotificationRule rule = 
                _notificationRules
                 .FirstOrDefault(r => r.Status(cakeOrder.OrderStatus));


            if (rule != null)
            {
                serviceResponse = await rule.Notify(cakeOrder);
            }

            return serviceResponse;
        }

    }

}

