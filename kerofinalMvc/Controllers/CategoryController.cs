using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repoitry;
using System.Diagnostics.CodeAnalysis;

namespace Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepositry repo;
        private readonly IProductRepositry productRepository;
        public CategoryController(ICategoryRepositry _repo , IProductRepositry _productRepo)
        {
            repo = _repo;
            productRepository = _productRepo;
        }


        public IActionResult Index()
        {
            List<Category> categories = repo.GetAll(c=>c.IsDeleted==false);
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            Category category = repo.GetById(id);
            //productList
            return View(category);
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult New(Category category)
        {
            //ProductList
            if (ModelState.IsValid == true)
            {
                //category.Prodects = new List<prodect>();
                repo.Add(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if(ModelState.IsValid == true)
            {
                repo.Update(id, category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Category category=repo.GetById(id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            category.IsDeleted = true;
            //int id = (int)category.Id;
            //repo.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult GetProductsByCategoryName(string categoryName)
        {
            List<prodect> prodects = productRepository.GetAllByCategoryName(categoryName);
            return View(prodects);
        }
    }
}
