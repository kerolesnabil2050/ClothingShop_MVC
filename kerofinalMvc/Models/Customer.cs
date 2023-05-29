using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Customer
    {
        [ForeignKey("ApplcationUser")]
        [Key]
        public string ApplcationUserId { get; set; }
       public string? Image { get; set; }
        public virtual List<Order>? Orders { get;set; }
        public virtual ApplcationUser? ApplcationUser { get; set; }
        [DefaultValue("false")]
        public virtual List<CustomerProduct>? CustomerProducts { get; set; }
       
        public Boolean IsDelete { get; set; }
    }
}
