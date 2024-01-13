using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Data.Repository;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public HomeController(ILogger<ProductController> logger, IProductRepository repository, ICategoryRepository category)
        {
            _logger = logger;
            _productRepository = (ProductRepository)repository;
            _categoryRepository = (CategoryRepository)category;
        }
        public IActionResult Index()
        {
            ViewBag.Category = _categoryRepository.GetListAsync().Result;
            return View(_productRepository.GetListAsync().Result);
        }
        public IActionResult ViewMore(int id)
        {
            return View(_productRepository.GetAsyncById(id).Result);
        }

        public IActionResult IndexByCategory(int id)
        {
            ViewBag.Category = _categoryRepository.GetListAsync().Result;
            return View(_productRepository.GetListAsync().Result.Where(i=>i.CategoryId == id).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
