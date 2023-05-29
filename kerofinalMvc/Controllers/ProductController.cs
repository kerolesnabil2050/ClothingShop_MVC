using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repoitry;
using Project.ViewModels;
using System.Data.Common;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        //Product/Index
        private readonly UserManager<ApplcationUser> userManager;

        private IProductRepositry ProductRepositry;
        private IProductSellerRepositry ProductSellerRepositry;
        private ICategoryRepositry CategoryRepositry;
        private IProductSellerRepositry productSellerRepositry;
        public ProductController(UserManager<ApplcationUser> userManager, IProductRepositry ProductRepositry, IProductSellerRepositry ProductSellerRepositry,
           ICategoryRepositry CategoryRepositry, IProductSellerRepositry productSellerRepositry)
        {
            this.userManager = userManager;
            this.ProductRepositry = ProductRepositry;
            this.ProductSellerRepositry = ProductSellerRepositry;
            this.CategoryRepositry = CategoryRepositry;
            this.productSellerRepositry = productSellerRepositry;
        }
        /// [Authorize(Roles = "Supplier")]
        public IActionResult Index()
        {

            //List<prodect> pro = ProductRepositry.GetAllwithinclude(c => c.IsDelete == false, "ProdectSeller");
            List<ProdectSeller> pro = ProductSellerRepositry.GetAllwithinclude(c => c.IsDelete == false, "Prodect");
            return View(pro);
        }
        //[Authorize(Roles = "Supplier")]
        public IActionResult Add()
        {
            ViewData["category"] = CategoryRepositry.GetAll(c => c.IsDeleted == false);

            return View();
        }

        //[Authorize(Roles = "Supplier")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProductSellerViewModel Newproduct)
        {
            var currentuser = await userManager.GetUserAsync(User);

            if (Newproduct != null && ModelState.IsValid)
            {


                string ImgePath = Directory.GetCurrentDirectory() + "/wwwroot/Fiels/imges";

                string ImgeName = Guid.NewGuid() + Path.GetFileName(Newproduct.Photo.FileName);

                string FinalIamgePath = Path.Combine(ImgePath, ImgeName);


                using (var Stream = new FileStream(FinalIamgePath, FileMode.Create))

                {
                    Newproduct.Photo.CopyTo(Stream);

                }
                prodect Oldprodect = ProductSellerRepositry.GetProductByName(Newproduct.Name, currentuser.Id);
             

                ProdectSeller prodectSeller = new ProdectSeller();

                if (Oldprodect != null)
                {
                    prodectSeller.ProdectId = Oldprodect.Id;

                }
                else
                {
                    prodect product1 = new prodect();
                    product1.Name = Newproduct.Name;
                    product1.Description = Newproduct.Description;
                    product1.Brand = Newproduct.Brand;
                    product1.Type = Newproduct.Type;
                    product1.CategoryId = Newproduct.CategoryId;
                    ProductRepositry.Add(product1);
                    prodectSeller.ProdectId = product1.Id;

                }
                prodectSeller.SellerId = currentuser.Id;
                prodectSeller.Quntity = Newproduct.Quntity;
                prodectSeller.Size = Newproduct.Size;
                prodectSeller.Img = Newproduct.Img;
                prodectSeller.Color = Newproduct.Color;
                prodectSeller.Price = Newproduct.Price;
                prodectSeller.Img = ImgeName;
                ProductSellerRepositry.Add(prodectSeller);
                return RedirectToAction("GetAllProducts", "Supplier");
            }
            return View(Newproduct);
        }

        public IActionResult GETSpecificItem([FromRoute] int id)
        {
            //Product / GETSpecificItem
            SpecificItemViewModel specificItem = new SpecificItemViewModel();
            ProdectSeller prodectSeller = productSellerRepositry.GetspecificItem(id, "Prodect");
            List<ProdectSeller> Products = productSellerRepositry.GetspecificItems(id);
            if (prodectSeller != null)
            {
                specificItem.Name = prodectSeller.Prodect.Name;
                specificItem.Type = prodectSeller.Prodect.Type;
                specificItem.Brand = prodectSeller.Prodect.Brand;
                specificItem.Quntity = prodectSeller.Quntity;
                specificItem.Price = prodectSeller.Price;
                specificItem.Imge = prodectSeller.Img;
                specificItem.Description = prodectSeller.Prodect.Description;
                specificItem.ProductId = id;
            }
            if (Products != null)
            {
                List<string> Colors = new List<string>();
                List<string> Size = new List<string>();
                foreach (ProdectSeller seller in Products)
                {
                    Colors.Add(seller.Color);
                    Size.Add(seller.Size);
                }

                ViewData["Colors"] = Colors;
                ViewData["Size"] = Size;

                return View(specificItem);
            }
            return Content("No");
        }

    }
}