using System;
using System.Net;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Models.Entities;
using CloudCityCakesMVC.Models.Enums;
using CloudCityCakesMVC.Models.Helpers;
using CloudCityCakesMVC.Services.Interfaces;
using SendGrid.Helpers.Mail;


namespace CloudCityCakesMVC.Services.Rules
{
    public class AcceptedNotificationRule : IStatusNotificationRule
    {
        private readonly IEmailService _emailService;

        public AcceptedNotificationRule(IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }
        
        public async Task<ServiceResponse> Notify(CakeOrder order)
        {
            var email = CreateEmail(order);
            var emailResponse =await _emailService
                .SendEmailAsync(email)
                .ConfigureAwait(false);
            
            var response = new ServiceResponse
            {
                Message = "Invoice has been emailed",
                ServiceResponseStatus = emailResponse.StatusCode == HttpStatusCode.Accepted ? ServiceResponseStatus.Ok : ServiceResponseStatus.FailUnhandled
            };
            
            return response;
        }

        public bool Status(OrderStatus status)
        {
            return status == OrderStatus.Accepted;
        }
        
        private Email CreateEmail(CakeOrder order)
        {
            var myEmail = "layla.porter@gmail.com";
            var body = $"Hello {order.User.Name}, Your cake order has been accepted. Please find your invoice attached";
            var invoicePath = CreateInvoice();
            var email = new Email
            {
                To = new EmailAddress(order.User.Email, order.User.Email),
                From = new EmailAddress(myEmail, "Cloud City Cake Co."),
                Subject = $"Your invoice for order id {order.Id}",
                HtmlContent = $"<strong>{body}<strong>",
                PlainTextContent = body,
                Filename = $"{order.Id}.pdf",
                FilePath = invoicePath
            };

            return email;
        }
        
        private string CreateInvoice()
        {
            return @"/Users/lporter/Documents/Repos/CloudCityCakeCo/BackendCSharp/src/CloudCityCakes.Api/invoice.pdf";
        }
    }
}