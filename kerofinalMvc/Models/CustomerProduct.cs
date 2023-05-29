using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class CustomerProduct
    {
        //private decimal sub;
        public int Id { get; set; } //string
        public int Quantity { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        [ForeignKey("ProdectSeller")]
        public int ProdectSellerId { get; set; }
        public decimal SubTotal
        {
            get;
            set;
        }
        [DefaultValue("false")]

        public Boolean IsDelete { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ProdectSeller? ProdectSeller { get; set; }
    }
}
