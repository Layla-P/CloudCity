using System;
using System.IO;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Services.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace CloudCityCakesMVC.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly SendGridAccount _account;

        public EmailService(IOptions<SendGridAccount> account)
        {
            _account = account.Value ?? throw new ArgumentNullException(nameof(account));
        }
        
        public async Task<Response> SendEmailAsync(Email email)
        {
            var client = new SendGridClient(_account.ApiKey);
            
            var msg = MailHelper
                .CreateSingleEmail(
                email.From,
                email.To,
                email.Subject,
                email.PlainTextContent,
                email.HtmlContent);
            
            msg.AddAttachment(email.Filename,
                EncodedAttachment(email.FilePath));
            
            var response = await client.SendEmailAsync(msg);
            
            return response;
        }

        private string EncodedAttachment(string path)
        {
            Byte[] bytes = File.ReadAllBytes(path);
            String file = Convert.ToBase64String(bytes);
            return file;
        }
    }

}