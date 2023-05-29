using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repoitry;
using Project.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace Project.Controllers
{
    public class CustomerActionsController : Controller
    {
        //CustomerActions/ShowProfile
        // private readonly Irepositry<Customer> repositry;
        private Context Context;
        private readonly UserManager<ApplcationUser> userManager;
        private readonly ICustomerRepositry customerRepositry;
        //private readonly SignInManager<ApplcationUser> SignInManager;
        Customer? obj = new Customer();
        public CustomerActionsController(Context Context, ICustomerRepositry _customerRepositry, UserManager<ApplcationUser> _userManager)
        {
            //repositry = _repositry;
            userManager = _userManager;
            customerRepositry = _customerRepositry;
            this.Context=Context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(customerRepositry.GetAll(e=>e.IsDelete==false));
        }
      
        public async Task<IActionResult> ShowProfile()
        {
            var currentUser = await userManager.GetUserAsync(User);
            string id = currentUser.Id;
            try
            {
                if (currentUser != null)
                {
                   obj =Context.Customer.FirstOrDefault(e => e.ApplcationUserId == id);
                    //obj = customerRepositry.GetCurrentUser(id);
                }
                return View(obj);

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }


    }
}
