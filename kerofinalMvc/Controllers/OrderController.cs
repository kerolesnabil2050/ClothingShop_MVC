using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repoitry;

namespace Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShipeerRepositry shipeerRepositry;
        private readonly IOrderRepositry orderRepositry;
        private readonly UserManager<ApplcationUser> userManager;
        private readonly IcustomerProductRepoitry icustomerProductRepoitry;
        private readonly IOrderProductCustomerRepostry orderProductCustomer;
        private readonly IProductSellerRepositry sellerRepositry;

        public OrderController(IProductSellerRepositry sellerRepositry, IOrderProductCustomerRepostry orderProductCustomer, IShipeerRepositry shipeerRepositry, IOrderRepositry orderRepositry, UserManager<ApplcationUser> userManager, IProductSellerRepositry productSellerRepositry, IcustomerProductRepoitry icustomerProductRepoitry)
        {
            this.shipeerRepositry = shipeerRepositry;
            this.orderRepositry = orderRepositry;
            this.userManager = userManager;
            this.icustomerProductRepoitry = icustomerProductRepoitry;
            this.orderProductCustomer = orderProductCustomer;
            this.sellerRepositry = sellerRepositry;
        }
        decimal TotalPrice = 0;

        public async Task<IActionResult> CheckOutt()
        {
            var currentuser = await userManager.GetUserAsync(User);
            if (currentuser != null)
            {
                ViewData["Date"] = DateTime.Now;
                List<CustomerProduct> Products = icustomerProductRepoitry.GetAllItemForUser(currentuser.Id);
                return View(Products);
            }
            else
            {
                return RedirectToAction("Login", "LoginAndSignOut");
            }


        }
        public async Task<IActionResult> PAyment()
        {
            //[FromRoute] List<CustomerProduct>
            var currentuser = await userManager.GetUserAsync(User);

            List<CustomerProduct> Products = icustomerProductRepoitry.GetAllItemForUser(currentuser.Id);
            List<ProdectSeller> prodectSellers = sellerRepositry.GetspecificItems(Products[0].ProdectSeller.ProdectId);
            if (Products != null)
            {
                Order order = new Order();
                order.Date = DateTime.Now;
                order.CustomerID = Products[0].CustomerId;
                order.ShiperID = shipeerRepositry.GetShiper();
                foreach (var item in Products)
                {
                    TotalPrice += (item.ProdectSeller.Price * item.Quantity);

                }
                order.TotalPrice = TotalPrice;

                orderRepositry.Add(order);

                foreach (var item in Products)
                {
                    OrderProdectSeller customerOrder = new OrderProdectSeller();
                    customerOrder.OrderId = order.Id;
                    customerOrder.ProdectSellerID = item.ProdectSellerId;
                    customerOrder.Quntity = item.Quantity;
                    orderProductCustomer.Add(customerOrder);
                    ProdectSeller seller = sellerRepositry.GetById(item.ProdectSellerId);
                    seller.Quntity = seller.Quntity - item.Quantity;
                    if(seller.Quntity==0)
                    {
                        seller.IsDelete = true;   
                    }
                   
                }
                foreach (CustomerProduct product in Products)
                {
                    product.IsDelete = true;
                    icustomerProductRepoitry.Updatesup(product.Id, product);
                }
                return View();

            }
            return Content("You Don't Have Products");

        }

        public async Task<IActionResult> CancelOrder()
        {
            var currentuser = await userManager.GetUserAsync(User);
           
            List<CustomerProduct> Products = icustomerProductRepoitry.GetAllItemForUser(currentuser.Id);
            if(Products!=null)
            {
                foreach(CustomerProduct product in Products) 
                {
                    product.IsDelete = true;
                    icustomerProductRepoitry.Update(product.Id, product);
                }
                return RedirectToAction("Index", "Product");
            }
            return Content("You Don't HAve Product");

        }




    }
}
