using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repoitry;

namespace Project.Controllers
{
    public class ShiperController : Controller
    {
        //Shiper/Index
        private readonly UserManager<ApplcationUser> userManager;

        private readonly  IShipeerRepositry shipeerRepositry;
        public ShiperController(IShipeerRepositry shipeerRepositry, UserManager<ApplcationUser> userManager)
        {
            this.shipeerRepositry = shipeerRepositry;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Shiper> shipers= shipeerRepositry.GetAllWithInclude("ApplcationUser");
            return View(shipers);
        }
        public IActionResult Welcoming()
        {
            return View();
        }
        public async Task<IActionResult> ShowProfile()
        {
            var currentUser = await userManager.GetUserAsync(User);
            string id = currentUser.Id;
            Shiper obj = new Shiper();
            try
            {
                if (currentUser != null)
                {
                    obj = shipeerRepositry.GetssupplierById(id);
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
            Shiper shiper = shipeerRepositry.GETSpecificsShiper(id);
            shiper.status = true;
            shipeerRepositry.Updatesup(id, shiper);
            return RedirectToAction("Index");

        }

        public IActionResult deletestate([FromRoute] string id)
        {
            Shiper shiper = shipeerRepositry.GETSpecificsShiper(id);
            shiper.IsDelete = true;
            shipeerRepositry.Updatesup(id, shiper);
            return RedirectToAction("Index");

        }

        public IActionResult ReturnSupplier([FromRoute] string id)
        {
            Shiper shiper = shipeerRepositry.GETSpecificsShiper(id);
            shiper.IsDelete = false;
            shipeerRepositry.Updatesup(id,shiper );
            return RedirectToAction("Index");
        }


    }
}
