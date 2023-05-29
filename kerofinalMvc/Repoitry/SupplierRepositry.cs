using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repoitry
{
    public class SupplierRepositry : Repositry<Seller>,IsupplierRepositry
    {
        private readonly Context context;
        public SupplierRepositry(Context context) : base(context)
        {
            this.context=context;
        }

		public Seller GetsellerById(string id)
		{
            return context.Sellers.FirstOrDefault(e => e.ApplcationUserId == id);
		}
        public List<Seller> GetAllWithInclude(string include)
        {

            return context.Sellers.Where(e => e.ApplcationUserId != null).Include(include).ToList();
        }

        public Seller GETSpecificsupplier(string id)
        {
            return context.Sellers.FirstOrDefault(e => e.ApplcationUserId == id);
        }

        public void Updatesup(string id, Seller s)
        {
            context.Sellers.Update(s);
            context.SaveChanges();
        }
    }
}
