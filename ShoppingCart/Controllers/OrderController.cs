using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data.Repository;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository, ICartRepository cartRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            var userId = _userRepository.GetAsyncByName(User.Identity.Name).Result.Id;
            ViewBag.ProductList = _productRepository.GetListAsync().Result;
            ViewBag.CartList = _cartRepository.GetListAsync().Result.Where(i => i.CartId == userId).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(OrderModel item)
        {
            var order = new Order()
            {
                Name = item.Name,
                Surname= item.Surname,
                Address = item.Region + ", " + item.City + ", " + item.StreetAddress + ", " + item.Address,
                PhoneNumber= item.PhoneNumber,
                Price= 0,
                Quantity = 0
            };
            _orderRepository.InsertAsync(order);
            ClearCart();
            return RedirectToAction("Gratitude");
        }

        private void ClearCart()
        {
            var userId = _userRepository.GetAsyncByName(User.Identity.Name).Result.Id;
            foreach (var cart in _cartRepository.GetListAsync().Result.Where(i => i.CartId == userId).ToList())
                _cartRepository.RemoveAsync(cart);
        }

        public ActionResult Gratitude()
        {
            return View();
        }
    }
}
