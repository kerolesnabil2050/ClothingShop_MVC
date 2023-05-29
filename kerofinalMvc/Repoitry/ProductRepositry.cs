using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repoitry
{
    public class ProductRepositry : Repositry<prodect>, IProductRepositry
    {
        private Context context;

        public ProductRepositry(Context context) : base(context)
        {
            this.context = context;
        }

        public prodect GetByIdWithInclude(int id, string include)
        {
            return context.prodect.Where(e => e.Id == id).Include(include).FirstOrDefault();
        }
        public List<prodect> GetAllByCategoryName(string categoryName)
        {
            List<prodect> prodects =
                context.prodect.Include(p => p.Category).
                Where(p => p.Category.Name == categoryName).ToList();
            return prodects;
        }

        public List<prodect> GetAllwithinclude(Func<prodect, bool> predicate, string include)
        {
            return context.prodect.Where(e => e.IsDelete == false).Include(include).ToList(); 
            
        }
    }
}
