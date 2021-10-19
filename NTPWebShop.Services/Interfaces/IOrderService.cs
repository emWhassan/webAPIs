using NTPWebShop.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTPWebShop.Services.Interfaces
{
    public interface IOrderService:IDisposable
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<bool> OrderExistsAsync(int OrderId);
        Task<Order> GetOrderAsync(int orderId);
        void AddOrder(Order addOrder);
        Task<bool> SaveChangesAsync();
    }
}
