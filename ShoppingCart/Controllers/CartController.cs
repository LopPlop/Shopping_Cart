using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data.Contexts;
using ShoppingCart.Data.Repository;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public CartController(ApplicationDbContext context, ICartRepository cartRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _context= context;
            _cartRepository= cartRepository;
            _userRepository= userRepository;
            _productRepository= productRepository;
        }
  
        public ActionResult Index()
        {
            var userId = _userRepository.GetAsyncByName(User.Identity.Name).Result.Id;
            List<Cart> cartList = new List<Cart>();
            if (_cartRepository.GetListAsync().Result.Capacity>0 && _cartRepository.GetListAsync().Result.Where(i => i.CartId == userId) is not null)
            {
                cartList = _cartRepository.GetListAsync().Result.Where(i => i.CartId == userId).ToList();
            }
            ViewBag.ProductList = _productRepository.GetListAsync().Result;
            return View(cartList);
        }

        public ActionResult Add(int id)
        {
            if(!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var userId = _userRepository.GetAsyncByName(User.Identity.Name).Result.Id;
            var cartId = _cartRepository.GetListAsync()?.Result.Where(i => i.CartId == userId)?.FirstOrDefault(i => i.ProductId == id)?.Id;
            _cartRepository.InsertAsync(new Cart() { CartId = userId, Quantity = 1, ProductId = id });
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Clear()
        {
            var userId = _userRepository.GetAsyncByName(User.Identity.Name).Result.Id;
            _cartRepository.ClearAsync(new Cart() { CartId = userId });
            return RedirectToAction("Index", "Cart");
        }
        
        public ActionResult Remove(int id)
        {
            var userId = _userRepository.GetAsyncByName(User.Identity.Name).Result.Id;
            var cart = _cartRepository.GetListAsync().Result.Where(i => i.CartId == userId).FirstOrDefault(i => i.ProductId == id);
            _cartRepository.RemoveAsync(cart);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
