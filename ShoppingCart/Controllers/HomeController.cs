using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.IO.Pipelines;
using System.Xml.Linq;
using ShoppingCart.Data.Contexts;
using ShoppingCart.Data.Repository;
using SQLitePCL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository repository, ICategoryRepository category)
        {
            _logger = logger;
            _productRepository = (ProductRepository)repository;
            _categoryRepository = (CategoryRepository)category;
        }

        public IActionResult Index()
        {
            return View(_productRepository.GetListAsync().Result);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(Product product)
        {
            if(ModelState.IsValid)
            {
                _productRepository.InsertAsync(product);
                _productRepository.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_productRepository.GetAsyncById(id).Result);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.UpdateAsync(product);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _productRepository.RemoveAsync(_productRepository.GetAsyncById(id).Result);
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}