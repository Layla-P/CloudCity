using System;
using System.Linq;
using System.Threading.Tasks;
using CloudCityCakesMVC.Models.Entities;

namespace CloudCityCakesMVC.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByPhoneNumber(string number);
        Task<User> GetByEmail(string email);
        Task<User> Add(User user);
        Task<User> Update(User user);
    }
}