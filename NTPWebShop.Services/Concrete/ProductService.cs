using NTPWebShop.Data;
using NTPWebShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NTPWebShop.Domain;

namespace NTPWebShop.Services.Concrete
{
    public class ProductService : IProductService
    {
        private NTPWebShopDBContext _context;

        public ProductService(NTPWebShopDBContext context)
        {
            _context = context;
        }

        public async Task<Domain.Product> GetProductAsync(int? roductId)
        {

            try
            {
                return await _context.Products.FindAsync(roductId);
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        public async Task<IEnumerable<Domain.Product>> GetProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync() > 0);
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

        

        public void AddProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new ArgumentNullException(nameof(product));
                }

                _context.Add(product);
                _context.SaveChanges();
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }


        public async Task<bool> ProductExistsAsync(int? productId)
        {
            try
            {
                return await _context.Products.AnyAsync(a => a.ProductId == productId);
            }
            catch (Exception exp)
            {

                throw exp;
            }
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

        public void UpdateProduct(int? productid, Product product)
        {

            try
            {
                var entity = _context.Products.Find(product.ProductId);
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(product));
                }
                _context.Entry(entity).CurrentValues.SetValues(product);
                _context.SaveChanges();
            }
            catch (Exception exp)
            {

                throw exp;
            }

           
        }
    }
}
