using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.ViewModels;

namespace Project.Controllers
{
    public class CustomerController : Controller
    {
        //Customer/Register

        private readonly UserManager<ApplcationUser> userManager;
        private readonly SignInManager<ApplcationUser> signInManager;
        private readonly Context context;
        public CustomerController(UserManager<ApplcationUser> _userManager, SignInManager<ApplcationUser> _signInManager, Context context)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(VeiwModelCustomer NewCustomer)
        {
            if (ModelState.IsValid)
            {
                string ImgePath = Directory.GetCurrentDirectory() + "/wwwroot/Fiels/imges";

                string ImgeName = Guid.NewGuid() + Path.GetFileName(NewCustomer.Photo.FileName);

                string FinalIamgePath = Path.Combine(ImgePath, ImgeName);


                using (var Stream = new FileStream(FinalIamgePath, FileMode.Create))

                {
                    NewCustomer.Photo.CopyTo(Stream);

                }
                ApplcationUser AppUser = new ApplcationUser();
                AppUser.FirstName = NewCustomer.FirstName;
                AppUser.LastName = NewCustomer.LastName;
                AppUser.Email = NewCustomer.Email;
                AppUser.PasswordHash = NewCustomer.Password;
                AppUser.Address = NewCustomer.Address;
                AppUser.UserName = NewCustomer.UserName;
                AppUser.PhoneNumber = NewCustomer.Phone;
                AppUser.Gender = NewCustomer.Gender;
                AppUser.Image = ImgeName;


                IdentityResult result = await userManager.CreateAsync(AppUser, NewCustomer.Password);
                Customer customer = new Customer();
                customer.ApplcationUserId = AppUser.Id;
                customer.Image = ImgeName;
                context.Customer.Add(customer);
                context.SaveChanges();
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(AppUser, false);
                    await userManager.AddToRoleAsync(AppUser, "Customer");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                }
            }
            return View(NewCustomer);

        }
    }
}
