using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.ViewModels;
using System.IO;
namespace Project.Controllers
{
    public class ShipperRegisterController : Controller
    {

        private readonly UserManager<ApplcationUser> userManager;
        private readonly SignInManager<ApplcationUser> SignInManager;
        private readonly Context context;
        public ShipperRegisterController(UserManager<ApplcationUser> userManager, SignInManager<ApplcationUser> signInManager, Context context)
        {
            this.userManager = userManager;
            this.SignInManager = signInManager;
            this.context = context;
        }
        //ShipperRegister/Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ShiperViewModel VmShiper)
        {

            if (ModelState.IsValid) 
            {
                string ImgePath = Directory.GetCurrentDirectory() + "/wwwroot/Fiels/imges";

               string ImgeName = Guid.NewGuid() + Path.GetFileName(VmShiper.Photo.FileName);

                string FinalIamgePath = Path.Combine(ImgePath, ImgeName);


                using (var Stream = new FileStream(FinalIamgePath, FileMode.Create))

                {
                    VmShiper.Photo.CopyTo(Stream);
                }

                ApplcationUser application = new ApplcationUser();
                application.UserName = VmShiper.UserName;
                application.PasswordHash = VmShiper.PassWord;

                IdentityResult result = await userManager.CreateAsync(application, VmShiper.PassWord);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(application, "Shipper");
                    //await signInManager.SignInWithClaimsAsync(application, false);
                    await SignInManager.SignInAsync(application, false);
                                                                         
                    Shiper Shiper = new Shiper();
                    Shiper.Age = VmShiper.Age;
                    Shiper.LicenseImg = ImgeName;
                    Shiper.SSD = VmShiper.SSD;
                    Shiper.Salary = 3000;
                    Shiper.ApplcationUserId = application.Id;
                    context.Shipers.Add(Shiper);
                    context.SaveChanges();
                    return RedirectToAction("Welcoming", "Shiper");
                }
                else
                {
                    //some issue ==>send user view
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(VmShiper);
                }

            }
            return View();
        }

       
        
    }
}
