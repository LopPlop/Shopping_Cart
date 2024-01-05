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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository repository, ICategoryRepository category)
        {
            _logger = logger;
            _productRepository = (ProductRepository)repository;
            _categoryRepository = (CategoryRepository)category;
        }
        public IActionResult Index()
        {
            try
            {
                string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                if (role != "admin")
                    return Content($"You have no permission");
            }
            catch(Exception ex)
            {
                return Content($"You have no permission");
            }

            ViewBag.Categories = _categoryRepository.GetListAsync().Result;
            return View(_productRepository.GetListAsync().Result);
        }

        public IActionResult Create()
        {
            var categories = new SelectList(_categoryRepository.GetListAsync().Result.OrderBy(c => c.Name)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewBag.CategoryId = categories;
            return base.View();
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
            var categories = new SelectList(_categoryRepository.GetListAsync().Result.OrderBy(c => c.Name)
            .ToDictionary(us => us.Id, us => us.Name), "Key", "Value");
            ViewBag.CategoryId = categories;
            return View(_productRepository.GetAsyncById(id).Result);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
                _productRepository.UpdateAsync(product);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _productRepository.RemoveAsync(_productRepository.GetAsyncById(id).Result);
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