using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.ViewModels;
using System.Data;

namespace Project.Controllers
{
    public class LoginAndSignOutController : Controller
    {
        //LoginAndSignOut/Login
        private readonly UserManager<ApplcationUser> userManager;
        private readonly SignInManager<ApplcationUser> SignInManager;
        private readonly Context context;
        public LoginAndSignOutController(UserManager<ApplcationUser> userManager, SignInManager<ApplcationUser> signInManager, Context context)
        {
            this.userManager = userManager;
            this.SignInManager = signInManager;
            this.context = context;
        }
        //LoginAndSignOut/Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userVmReq) 
        {
            if (ModelState.IsValid)
            {
                ApplcationUser userModel = await userManager.FindByNameAsync(userVmReq.Username);
                if (userModel != null)
                {

                    Microsoft.AspNetCore.Identity.SignInResult result =
                  await SignInManager.PasswordSignInAsync(userModel, userVmReq.Password, userVmReq.rememberMe, false);

                    if (result.Succeeded && await userManager.IsInRoleAsync(userModel, "Supplier"))
                    {
                        Seller seller = context.Sellers.Where(x => x.ApplcationUser.UserName == userModel.UserName).FirstOrDefault();
                        if(seller.status==true)
                        {
                        return RedirectToAction("GetAllProducts", "Supplier");

                        }
                        else
                        {
                            return RedirectToAction("Welcoming", "Shiper");

                        }
                        // return Content("Supplier");
                    }
                    else if (result.Succeeded && await userManager.IsInRoleAsync(userModel, "CUSTOMER"))
                    {
                        
                        return RedirectToAction("Index", "Product");

                        //return Content("CUSTOMER");
                    }
                    else if (result.Succeeded && await userManager.IsInRoleAsync(userModel, "ADMIN"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (result.Succeeded && await userManager.IsInRoleAsync(userModel, "SHIPPER"))
                    {
                        return RedirectToAction("Welcoming", "Shiper");
                    }
                    else
                    {
                        return Content("Error");

                    }

                }
            }
       
                return View(userVmReq);
        }
        public async Task<IActionResult> signOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index","Product");
        }
    }
}
