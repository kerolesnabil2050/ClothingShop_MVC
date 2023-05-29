using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Project.Models;
using Project.Repoitry;
using Project.ViewModels;
using System;

namespace Project.Controllers
{
    public class CardController : Controller
    {
        private readonly UserManager<ApplcationUser> userManager;
        private readonly IProductSellerRepositry productSellerRepositry;
        private readonly IcustomerProductRepoitry icustomerProductRepoitry;

        //Card/ShowAllProductInCartAsync
        public CardController(UserManager<ApplcationUser> userManager, IProductSellerRepositry productSellerRepositry, IcustomerProductRepoitry icustomerProductRepoitry)
        {
            this.userManager = userManager;
            this.productSellerRepositry = productSellerRepositry;
            this.icustomerProductRepoitry = icustomerProductRepoitry;
        }
        public async Task<IActionResult> Add(SpecificItemViewModel EditProduct)
        {
            // LoginAndSignOut / Login
            var currentuser = await userManager.GetUserAsync(User);
            if (currentuser != null)
            {

                List<ProdectSeller> prodectSellers = productSellerRepositry.GetspecificItems(EditProduct.ProductId, "Prodect");
                List<CustomerProduct> ProductDetails = icustomerProductRepoitry.GetAllItemForUser(currentuser.Id);
                //Find Match
                bool ISExist = false;
                int sub = 0;

                foreach (ProdectSeller prodect in prodectSellers)
                {
                    if (prodect.Color == EditProduct.Color && prodect.Size == EditProduct.Size && EditProduct.Quntity <= prodect.Quntity)
                    {
                        if (ProductDetails.Count() > 0)
                        {
                            foreach (CustomerProduct item in ProductDetails)
                            {
                                if (item.ProdectSellerId == prodect.Id)
                                {
                                    icustomerProductRepoitry.CustomerProductModified(prodect.Id, currentuser.Id, EditProduct.Quntity);
                                    ISExist = true;
                                }
                            }
                        }
                        if (ProductDetails.Count() <= 0 || ISExist == false)
                        {
                            CustomerProduct customerProduct = new CustomerProduct();
                            customerProduct.Quantity = EditProduct.Quntity;
                            customerProduct.CustomerId = currentuser.Id;
                            sub = customerProduct.Quantity * prodect.Price;
                            customerProduct.SubTotal = sub;
                            customerProduct.ProdectSellerId = prodect.Id;
                            icustomerProductRepoitry.Add(customerProduct);
                        }
                        return RedirectToAction("ShowAllProductInCart", "Card");

                    }
                }

                return Content("Not Existance");
            }
            return RedirectToAction("Login", "LoginAndSignOut");
        }
        public async Task<IActionResult> ShowAllProductInCart()
    
        {
            //Card/ShowAllProductInCart
            var currentuser = await userManager.GetUserAsync(User);

            if (currentuser != null)
            { 
            List<CustomerProduct> customerProducts = icustomerProductRepoitry.GetAllItemForUser(currentuser.Id).ToList();
                if (customerProducts != null)
                {
                    return View(customerProducts);
                }
                else
                {
                    return Content("NOT Found");
                }
        }
            else
            {
                return RedirectToAction("Login", "LoginAndSignOut");

            }
        }
        public async Task<IActionResult> DeleteProductFromCard(int id)
        {
            var currentuser = await userManager.GetUserAsync(User);

            CustomerProduct customerProduct = icustomerProductRepoitry.SpecificCustomerProduct(id, currentuser.Id);
            customerProduct.IsDelete = true;
            icustomerProductRepoitry.Update(id, customerProduct);
            return RedirectToAction("ShowAllProductInCart", "Card");
        }


    }
}
