using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Repoitry
{
    public class CustomerProductRepositry : Repositry<CustomerProduct>, IcustomerProductRepoitry
    {
        private Context context;

        public CustomerProductRepositry(Context context) : base(context)
        {
            this.context = context;
        }

        public List<CustomerProduct> GetAllItemForUser(string id)
        {
            return context.customerProducts.Where(e => e.CustomerId == id &&e.IsDelete==false).Include(e=>e.ProdectSeller).ThenInclude(e=>e.Prodect).ToList();
        }
        public void CustomerProductModified(int ProductsellerId, string id, int NewQuantity)
        {
            CustomerProduct customer = context.customerProducts.Where(x => x.ProdectSellerId == ProductsellerId && x.CustomerId == id).FirstOrDefault();
            customer.Quantity = NewQuantity;
            context.SaveChanges();
        }

        public CustomerProduct SpecificCustomerProduct(int Id,string CustomerID)
        {
            return context.customerProducts.FirstOrDefault(x => x.ProdectSellerId == Id && x.CustomerId==CustomerID);
        }

        public void Updatesup(int id, CustomerProduct s)
        {

            context.customerProducts.Update(s);
            context.SaveChanges();
        }
    }
}
