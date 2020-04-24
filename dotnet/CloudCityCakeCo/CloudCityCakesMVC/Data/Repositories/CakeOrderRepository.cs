using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCityCakesMVC.Data.Context;
using CloudCityCakesMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudCityCakesMVC.Data.Repositories
{
    public class CakeOrderRepository : ICakeOrderRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CakeOrderRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public Task<IQueryable<CakeOrder>> GetAll()
        {
            return  Task.FromResult(_context.CakeOrders.AsQueryable());
        }
        
        public async Task<IEnumerable<CakeOrder>> GetAllByUserId(int userId)
        {
            return  await _context.CakeOrders
                .Include("User").Where(e=> e.UserId == userId).ToListAsync();
        }
        
        public async Task<CakeOrder> GetById(int id)
        {
            return  await _context.CakeOrders.Include("User")
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<CakeOrder> Add(CakeOrder cakeOrder)
        {
            var order = _context.CakeOrders.Add(cakeOrder);
            await _context.SaveChangesAsync();
            return  order.Entity;
        }

        public async Task<CakeOrder> Update(CakeOrder cakeOrder)
        {
            var order =_context.Update(cakeOrder);
            await _context.SaveChangesAsync();
            return order.Entity;
        }

        public async Task DeleteOrder(CakeOrder cakeOrder)
        {
            _context.CakeOrders.Remove(cakeOrder);
            await _context.SaveChangesAsync();
        }
    }
}