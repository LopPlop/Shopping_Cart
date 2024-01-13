using ShoppingCart.Data.Contexts;
using ShoppingCart.Models;

namespace ShoppingCart.Data.Repository
{

    public interface ICartRepository : IDisposable
    {
        Task<List<Cart>> GetListAsync();
        Task<Cart> GetAsyncById(int id);
        Task InsertAsync(Cart item);
        Task RemoveAsync(Cart item);
        Task UpdateAsync(Cart item);
        Task ClearAsync(Cart item);
    }


    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;
        public CartRepository(ApplicationDbContext context) => _context = context;

        public async Task<Cart> GetAsyncById(int id) => await _context.Cart.FirstAsync(c => c.Id == id);

        public async Task<List<Cart>> GetListAsync() => await _context.Cart.ToListAsync();

        public async Task InsertAsync(Cart item)
        {
            await _context.Cart.AddAsync(item);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(Cart item)
        {
            _context.Cart.Remove(item);
            await SaveChangesAsync();
        }

        public async Task ClearAsync(Cart item)
        {
            var currList = await GetListAsync();
            var temp = currList.Where(c => c.CartId == item.CartId).ToList();
            foreach(var obj in temp)
                await RemoveAsync(obj);
        }

        public async Task UpdateAsync(Cart item)
        {
            _context.Cart.Entry(item).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw; // повторное возбуждение исключения
                }
            }
        }
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
