using Project.Models;

namespace Project.Repoitry
{
    public interface IcustomerProductRepoitry : Irepositry<CustomerProduct>
    {
        List<CustomerProduct> GetAllItemForUser(string id);
        void CustomerProductModified(int ProductsellerId, string id, int NewQuantity);
     //Product seller ID
        CustomerProduct SpecificCustomerProduct(int Id, string CustomerID);
        void Updatesup(int id, CustomerProduct s);
    }
}
