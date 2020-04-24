using System.Threading.Tasks;
using CloudCityCakesMVC.Models.DTO;

namespace CloudCityCakesMVC.Services.Interfaces
{
    public interface IPriceCalculationService
    {
        Task<Cake> Total(Cake cake);
    }
}