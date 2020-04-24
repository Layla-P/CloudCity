using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;
using SendGrid;

namespace CloudCityCakesMVC.Services.Interfaces
{
    public interface IEmailService
    {
        Task<Response> SendEmailAsync(Email email);
    }
}