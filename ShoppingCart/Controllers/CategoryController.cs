using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCart.Data.Repository;
using ShoppingCart.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ShoppingCart.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(ILogger<ProductController> logger, IProductRepository repository, ICategoryRepository category)
        {
            _logger = logger;
            _productRepository = (ProductRepository)repository;
            _categoryRepository = (CategoryRepository)category;
        }
        
        public ActionResult Index()
        {
            try
            {
                string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                if (role != "admin" || role == null)
                    return Content($"You have no permission");
            }
            catch (Exception ex)
            {
                return Content($"You have no permission");
            }

            return View(_categoryRepository.GetListAsync().Result);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
                _categoryRepository.InsertAsync(category);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_categoryRepository.GetAsyncById(id).Result);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
                _categoryRepository.UpdateAsync(category);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _categoryRepository.RemoveAsync(_categoryRepository.GetAsyncById(id).Result);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return base.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
