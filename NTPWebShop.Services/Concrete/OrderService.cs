using NTPWebShop.Data;
using NTPWebShop.Domain;
using NTPWebShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace NTPWebShop.Services.Concrete
{
    public class OrderService : IOrderService
    {

        private NTPWebShopDBContext _context;

        public OrderService(NTPWebShopDBContext context)
        {
            _context = context;
        }
        public  void AddOrder(Order addOrder)
        {
            if (addOrder == null)
            {
                throw new ArgumentNullException(nameof(addOrder));
            }

            _context.Add(addOrder);
            _context.SaveChanges();
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public async Task<bool> OrderExistsAsync(int OrderId)
        {
            return await _context.Orders.AnyAsync(a => a.OrderId == OrderId);
        }
    }
}
