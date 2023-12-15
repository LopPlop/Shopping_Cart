using ShoppingCart.Data.Contexts;
using ShoppingCart.Models;
using SQLitePCL;

namespace ShoppingCart.Data.Repository
{
    public interface ICategoryRepository : IDisposable
    {
        Task<List<Category>> GetListAsync();
        Task<Category> GetAsyncById(int id);
        Task<Category> GetAsyncByName(string name);
        Task InsertAsync(Category product);
        Task RemoveAsync(Category product);
        Task UpdateAsync(Category product);
    }


    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;
        public CategoryRepository(ApplicationDbContext context) => _context = context;
        
        public async Task<Category> GetAsyncById(int id) => await _context.Categories.FirstAsync(c => c.Id == id);

        public async Task<Category> GetAsyncByName(string name) => await _context.Categories.FirstAsync(c => c.Name == name);

        public async Task<List<Category>> GetListAsync() => await _context.Categories.ToListAsync();

        public async Task InsertAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(Category category)
        {
            _context.Categories.Remove(category);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await SaveChangesAsync();
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
