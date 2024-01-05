using ShoppingCart.Data.Contexts;
using ShoppingCart.Models;

namespace ShoppingCart.Data.Repository
{
    public interface IUserRepository : IDisposable
    {
        Task<List<User>> GetListAsync();
        Task<User> GetAsyncById(int id);
        Task<User> GetAsyncByName(string name);
        Task InsertAsync(User item);
        Task RemoveAsync(User item);
        Task UpdateAsync(User item);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;
        public UserRepository(ApplicationDbContext context) => _context = context;

        public async Task<User> GetAsyncById(int id) => await _context.Users.FirstAsync(i => i.Id == id);

        public async Task<User> GetAsyncByName(string name) => await _context.Users.FirstAsync(i => i.Email == name);
        public async Task<List<User>> GetListAsync() => await _context.Users.ToListAsync();
        public async Task InsertAsync(User item)
        {
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(User item)
        {
            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User item)
        {
            _context.Users.Update(item);
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
