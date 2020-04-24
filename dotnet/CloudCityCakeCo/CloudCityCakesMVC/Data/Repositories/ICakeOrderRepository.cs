using System;
using System.Linq;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.Entities;

namespace CloudCityCakesMVC.Data.Repositories
{
    public interface ICakeOrderRepository
    {
        Task<IQueryable<CakeOrder>> GetAll();
        Task<CakeOrder> GetById(int id);
        Task<CakeOrder> Add(CakeOrder cakeOrder);
        Task<CakeOrder> Update(CakeOrder cakeOrder);
        Task DeleteOrder(CakeOrder cakeOrder);
    }
}