using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repoitry
{
    public class ShipperRepositry : Repositry<Shiper>,IShipeerRepositry
    {
        private readonly Context context;
        public ShipperRepositry(Context context) : base(context)
        {
            this.context = context; 
        }

        public string GetShiper()
        {
            return context.Shipers.FirstOrDefault(e => e.IsDelete == false && e.busy == false && e.status == true).ApplcationUserId;
        }

        public Shiper GetssupplierById(string id)
        {
            return context.Shipers.FirstOrDefault(e => e.ApplcationUserId == id);
        }
        public List<Shiper> GetAllWithInclude(string include)
        {


            return context.Shipers.Where(e => e.ApplcationUserId != null).Include(include).ToList();


        }

      

        public Shiper GETSpecificsShiper(string id)
        {

            return context.Shipers.FirstOrDefault(e => e.ApplcationUserId == id);

        }
        public void Updatesup(string id, Shiper s)
        {
            context.Shipers.Update(s);
            context.SaveChanges();
        }
    }

}
