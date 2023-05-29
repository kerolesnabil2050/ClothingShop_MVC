using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Linq;

namespace Project.Repoitry
{
    public class ProductSellereRepositry : Repositry<ProdectSeller>, IProductSellerRepositry
    {
        private Context context;
        public ProductSellereRepositry(Context context) : base(context)
        {
            this.context   = context;
        }
        public List<ProdectSeller> GetAllwithinclude(Func<prodect, bool> predicate, string include)
        {
            return context.prodectSellers.Where(e => e.IsDelete == false).Include(include).ToList();
        }

        public prodect? GetProductByName(string name, string idseller)
        {
            prodect dsd = context.prodect.Where(x => x.Name == name).FirstOrDefault();
            if (dsd != null)
            {
                ProdectSeller prodect = context.prodectSellers.Where(x => x.Prodect.Name == name && x.SellerId == idseller).Include(x => x.Prodect).FirstOrDefault();
                prodect prodect1 = prodect.Prodect;
                return prodect1;
            }
            else
            {
                return null;
            }
        }

        public ProdectSeller GetspecificItem(int id,string include)
        {
            return context.prodectSellers.Where(e => e.ProdectId == id).Include(include).FirstOrDefault();
        }

        public List<ProdectSeller> GetspecificItems(int id, string include = null)
        {
            return context.prodectSellers.Where(x => x.ProdectId==id).ToList();
        }
       public List<ProdectSeller> GetAllProductsForSeller(string SellerId)
        {
            return context.prodectSellers.Where(x => x.SellerId == SellerId&& x.IsDelete==false).Include(x=>x.Prodect).ToList();
        }

        public ProdectSeller EditProduct(int Id)
        {
            return context.prodectSellers.Where(x => x.Id == Id).Include(c=>c.Prodect).FirstOrDefault();
        }
    }
}
