using Microsoft.AspNetCore.Identity;
using Project.Models;

namespace Project.Repoitry
{
    public class CustomerRepositry : Repositry<Customer>, ICustomerRepositry
    {
        private readonly Context context;
        private readonly UserManager<ApplcationUser> userManager;
        public CustomerRepositry(Context context, UserManager<ApplcationUser> _userManager) : base(context)
        {
            this.context = context;
            this.userManager = _userManager;
        }
        public List<Order> CustomerOreder(string Id)
        {
            return context.Orders.Where(order => order.CustomerID == Id).ToList();
        }
        public Customer? GetCurrentUser(string Id)
        {
            return context.Customer.FirstOrDefault(e=>e.ApplcationUserId==Id);
        }
    
    }
}
