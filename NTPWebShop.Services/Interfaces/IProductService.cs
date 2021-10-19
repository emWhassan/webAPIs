using NTPWebShop.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NTPWebShop.Services.Interfaces
{

    public interface IProductService : IDisposable
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<bool> ProductExistsAsync(int? productId);
        Task<Product> GetProductAsync(int? roductId);
        void AddProduct(Product addProduct);
        void UpdateProduct(int? productid,Product product);
        Task<bool> SaveChangesAsync();
    }
}
