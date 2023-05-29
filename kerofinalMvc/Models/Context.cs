using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class Context:IdentityDbContext<ApplcationUser>       
    {
        public Context() :base(){ }    
        public Context (DbContextOptions options):base(options)
        {

        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProdectSeller> orderProdectSellers { get; set; }
        public DbSet<prodect> prodect { get; set; }
        public DbSet<Seller>Sellers { get; set; }
        public DbSet<Shiper> Shipers { get; set; }
        public DbSet<ProdectSeller> prodectSellers { get; set; }
        public DbSet<Comment> comments { get; set; }

        public DbSet<CustomerProduct> customerProducts { get; set; }

       
    }
}

