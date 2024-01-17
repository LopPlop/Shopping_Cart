using ShoppingCart.Data.Contexts;
using ShoppingCart.Models;

namespace ShoppingCart.Data.Repository
{
    public interface IOrderRepository : IDisposable
    {
        Task<List<Order>> GetListAsync();
        Task<Order> GetAsyncById(int id);
        Task InsertAsync(Order item);
        Task RemoveAsync(Order item);
        Task UpdateAsync(Order item);
       /* Task ClearAsync(Order item);*/
    }



    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;
        public OrderRepository(ApplicationDbContext context) => _context = context;

        public async Task<Order> GetAsyncById(int id) => await _context.Orders.FirstAsync(c => c.Id == id);

        public async Task<List<Order>> GetListAsync() => await _context.Orders.ToListAsync();

        public async Task InsertAsync(Order item)
        {
            await _context.Orders.AddAsync(item);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(Order item)
        {
            _context.Orders.Remove(item);
            await SaveChangesAsync();
        }

/*        public async Task ClearAsync(Order item)
        {
            var currList = await GetListAsync();
            var temp = currList.Where(c => c.CartId == item.CartId).ToList();
            foreach (var obj in temp)
                await RemoveAsync(obj);
        }*/

        public async Task UpdateAsync(Order item)
        {
            _context.Orders.Entry(item).State = EntityState.Modified;
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
