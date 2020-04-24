using System;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Models.Entities;
using CloudCityCakesMVC.Models.Enums;
using CloudCityCakesMVC.Models.Helpers;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CloudCityCakesMVC.Services.Rules
{
    public class CompletedNotificationRule : IStatusNotificationRule
    {
        private readonly TwilioAccount _twilioAccount;

        public CompletedNotificationRule(IOptions<TwilioAccount> account)
        {
            _twilioAccount = account.Value ?? throw new ArgumentNullException(nameof(account));
        }
        
        public Task<ServiceResponse> Notify(CakeOrder order)
        {
            TwilioClient.Init(_twilioAccount.AccountSid, _twilioAccount.AuthToken);

            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                body: $"Your  Cake order's status code is {order.OrderStatus.ToString()}",
                to: new Twilio.Types.PhoneNumber($"{order.User.PhoneNumber}")
            );
            
            var response = new ServiceResponse
            {
                Message = "Whatsapp message sent",
                ServiceResponseStatus = ServiceResponseStatus.Ok
            };
            return Task.FromResult(response);
        }

        public bool Status(OrderStatus status)
        {
            return status == OrderStatus.Completed;
        }
    }
}