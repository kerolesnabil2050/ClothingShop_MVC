using System.ComponentModel;

namespace Project.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Decimal TotalPrice { get; set; }
        public string CustomerID { get; set; }
        public string ShiperID { get; set; }
        public virtual Customer?  Customer{ get; set; }
        public virtual Shiper? Shiper { get; set; }
        [DefaultValue("false")]
        public Boolean IsDelete { get; set; }

    }
}
