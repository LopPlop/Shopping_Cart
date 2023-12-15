using System;
using NuGet.Common;
using ShoppingCart.Data.Contexts;
using ShoppingCart.Models;

namespace ShoppingCart.Data.Repository
{
    public interface IProductRepository : IDisposable
    {
        Task<List<Product>> GetListAsync();
        Task<Product> GetAsyncById(int id);
        Task<Product> GetAsyncByName(string name);
        Task InsertAsync(Product product);
        Task RemoveAsync(Product product);
        Task UpdateAsync(Product product);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;
        public ProductRepository(ApplicationDbContext context) => _context = context;
        public async Task<List<Product>> GetListAsync() => await _context.Products.ToListAsync();
        public async Task<Product> GetAsyncById(int id) => await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
        public async Task<Product> GetAsyncByName(string name) => await _context.Products.SingleOrDefaultAsync(p => p.Name == name);
        public async Task InsertAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
