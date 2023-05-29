using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.Hubs;
using Project.Models;
using Project.ViewModels;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Project.Controllers
{
    public class SupplierRegisterController : Controller
    {
        private readonly UserManager<ApplcationUser> userManager;
        private readonly SignInManager<ApplcationUser> SignInManager;
        private readonly Context context;
		IHubContext<SupplierHub> hubContext;

		public SupplierRegisterController(UserManager<ApplcationUser> userManager, SignInManager<ApplcationUser> signInManager,Context context,

			IHubContext<SupplierHub> hubContext
			)
        {
            //SupplierRegister/register
            this.userManager = userManager;
            this.SignInManager = signInManager;
            this.context = context;
            this.hubContext= hubContext;
        }
        public IActionResult register()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> register(SuplierRegisterViewModel suplierView)
       
        {
            if (ModelState.IsValid)
            {
                string ImgePath = Directory.GetCurrentDirectory() + "/wwwroot/Fiels/imges";

                string ImgeName = Guid.NewGuid() + Path.GetFileName(suplierView.Photo.FileName);

                string FinalIamgePath = Path.Combine(ImgePath, ImgeName);


                using (var Stream = new FileStream(FinalIamgePath, FileMode.Create))

                {
                    suplierView.Photo.CopyTo(Stream);

                }
                ApplcationUser application = new ApplcationUser();
                    await userManager.AddToRoleAsync(application, "Supplier");
                    Seller seller = new Seller();
                    application.UserName = suplierView.UserName;
                    application.Email = suplierView.Email;
                    application.FirstName = suplierView.FirstName;
                    application.LastName = suplierView.LastName;
                    application.PhoneNumber = suplierView.PhoneNumber;
                    application.Address = suplierView.Adress;
                    IdentityResult result = await userManager.CreateAsync(application, suplierView.PassWord);
                    await SignInManager.SignInAsync(application, false);

                if (result.Succeeded)
                { 
                    seller.Age = suplierView.Age;
                    seller.Img = ImgeName;
                    seller.SSD = suplierView.SSD;
                    seller.ApplcationUserId = application.Id;
                    context.Sellers.Add(seller);
                    context.SaveChanges();
					hubContext.Clients.All.SendAsync("NewSupplierRegister", seller);

                    return RedirectToAction("Welcoming", "Shiper");
                }
                else
                {
                    await userManager.DeleteAsync(application);
                }
            }
            return View(suplierView);
        }

       
       
    }
}
