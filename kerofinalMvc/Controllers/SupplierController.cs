using Microsoft.AspNetCore.Mvc;
using Project.Repoitry;
using Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Project.ViewModels;

namespace Project.Controllers
{
    public class SupplierController : Controller
    {
        //Supplier/GetAll
        //Supplier/ShowProfile

        private IsupplierRepositry IsupplierRepositry;
        private readonly IProductSellerRepositry productSellerRepositry;
        private readonly UserManager<ApplcationUser> userManager;
        private readonly IProductRepositry productRepositry;
        private ICategoryRepositry CategoryRepositry;

        public SupplierController(IProductRepositry productRepositry,
            UserManager<ApplcationUser> userManager, IsupplierRepositry IsupplierRepositry, IProductSellerRepositry productSellerRepositry, ICategoryRepositry categoryRepositry)
        {
            this.IsupplierRepositry = IsupplierRepositry;
            this.productSellerRepositry = productSellerRepositry;
            this.productSellerRepositry = productSellerRepositry;
            this.userManager = userManager;
            this.productRepositry = productRepositry;
            CategoryRepositry = categoryRepositry;
        }
        public async Task<ActionResult> GetAllProducts()
        {
            var currentuser = await userManager.GetUserAsync(User);

            List<ProdectSeller> prodectSellers = productSellerRepositry.GetAllProductsForSeller(currentuser.Id);
           
                return View(prodectSellers);
            

            //return RedirectToAction("Login", "LoginAndSignOut");
        }
        public IActionResult GetAll()
        {
            List<Seller> sellers = IsupplierRepositry.GetAllWithInclude("ApplcationUser");
            return View(sellers);
        }

        public IActionResult DeleteItem([FromRoute] int Id)
        {
            ProdectSeller prodect = productSellerRepositry.GetById(Id);
            prodect.IsDelete = true;
            productSellerRepositry.Update(Id, prodect);
            return RedirectToAction("GetAllProducts");
        }
        // productseller id
        public IActionResult EditProduct([FromRoute] int Id)
        {
            ProductSellerViewModel sellerViewModel = new ProductSellerViewModel();
            ProdectSeller prodectSeller = productSellerRepositry.EditProduct(Id);
            sellerViewModel.Name = prodectSeller.Prodect.Name;
            sellerViewModel.Brand = prodectSeller.Prodect.Brand;
            sellerViewModel.Description = prodectSeller.Prodect.Description;
            sellerViewModel.Type = prodectSeller.Prodect.Type;
            sellerViewModel.CategoryId = prodectSeller.Prodect.CategoryId;
            sellerViewModel.Quntity = prodectSeller.Quntity;
            sellerViewModel.Img = prodectSeller.Img;
            sellerViewModel.Color = prodectSeller.Color;
            sellerViewModel.Size = prodectSeller.Size;
            sellerViewModel.Price = prodectSeller.Price;
            ViewData["category"] = CategoryRepositry.GetAll(c => c.IsDeleted == false);

            return View(sellerViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct([FromRoute] int Id, ProductSellerViewModel productSellerView)
        {
            if (!ModelState.IsValid)
            {
                ProdectSeller prodectSeller = productSellerRepositry.EditProduct(Id);
                prodectSeller.Prodect.Name = productSellerView.Name;
                prodectSeller.Prodect.Type = productSellerView.Type;
                prodectSeller.Prodect.Description = productSellerView.Description;
                prodectSeller.Prodect.Brand = productSellerView.Brand;
                prodectSeller.Prodect.CategoryId = productSellerView.CategoryId;
                prodectSeller.Price = productSellerView.Price;
                prodectSeller.Img = productSellerView.Img;
                prodectSeller.Color = productSellerView.Color;
                prodectSeller.Size = productSellerView.Size;
                prodectSeller.Quntity = productSellerView.Quntity;
                productSellerRepositry.Update(Id, prodectSeller);
                productRepositry.Update(prodectSeller.ProdectId, prodectSeller.Prodect);

                return RedirectToAction("GetAllProducts");
            }
            return RedirectToAction("EditProduct");

        }
		public async Task<IActionResult> ShowProfile()
		{
			var currentUser = await userManager.GetUserAsync(User);
			string id = currentUser.Id;
            Seller obj = new Seller();
			try
			{
				if (currentUser != null)
				{
					obj = IsupplierRepositry.GetsellerById(id);
					//obj = customerRepositry.GetCurrentUser(id);
				}
				return View(obj);

			}
			catch (Exception ex)
			{
				return Content(ex.Message);
			}

		}
        public IActionResult changestate([FromRoute] string id)
        {
            Seller seller = IsupplierRepositry.GETSpecificsupplier(id);
            seller.status = true;
            IsupplierRepositry.Updatesup(id, seller);
            return RedirectToAction("GetAll");

        }

        public IActionResult deletestate([FromRoute] string id)
        {
            Seller seller = IsupplierRepositry.GETSpecificsupplier(id);
            seller.IsDelete = true;
            IsupplierRepositry.Updatesup(id, seller);
            return RedirectToAction("GetAll");

        }

        public IActionResult ReturnSupplier([FromRoute] string id)
        {
            Seller seller = IsupplierRepositry.GETSpecificsupplier(id);
            seller.IsDelete = false;
            IsupplierRepositry.Updatesup(id, seller);
            return RedirectToAction("GetAll");
        }
    }
}
