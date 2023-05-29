using Project.Models;

namespace Project.Repoitry
{
    public interface ICustomerRepositry:Irepositry<Customer>
    {
        List<Order> CustomerOreder(string Id);
        Customer? GetCurrentUser(string Id);
    
    }
}
