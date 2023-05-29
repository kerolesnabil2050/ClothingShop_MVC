using System.ComponentModel;

namespace Project.Models
{
    public class ProdectSeller 
    {
        public int Id { get; set; } 
        public int ProdectId { get; set; }
        public string SellerId { get; set; }
        public int Quntity { get;set; }
        public string Color { get; set; }
        public string Size { get; set; } 
        public string? Img { get; set; }
        public int Price { get; set; }
        public virtual prodect? Prodect { get; set; }    
        public virtual Seller? Seller { get; set; }

        [DefaultValue("false")]
        public Boolean IsDelete { get; set; }
        //public virtual List<CustomerProduct> CustomerProducts { get; set; }

    }
}
