using System;
using System.Linq;
using System.Threading.Tasks;
using CloudCityCakesMVC.Data.Context;
using CloudCityCakesMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudCityCakesMVC.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        
        private readonly ApplicationDbContext _context;
        
        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public  Task<IQueryable<User>> GetAll()
        {
            return  Task.FromResult(_context.Users.AsQueryable());
        }

        public async Task<User> GetById(int id)
        {
            return  await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetByPhoneNumber(string number)
        {
            return  await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber == number);
        }

        public async Task<User> GetByEmail(string email)
        {
            return  await _context.Users.FirstOrDefaultAsync(e => e.PhoneNumber == email);
        }

        public async Task<User> Add(User user)
        {
            var userEntity = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return  userEntity.Entity;
        }

        public async Task<User> Update(User user)
        {
            var userEntity = _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return  userEntity.Entity;
        }
    }
}