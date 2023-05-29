using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Project.Models
{
    public class Seller
    {
        [ForeignKey("ApplcationUser")]
        [Key]
        public string ApplcationUserId { get; set; }
        public int Age { get; set; } 
        public decimal? blance { get; set; }
   
        public string SSD { get; set; }  
        public string? Img { get; set; }
      
        public virtual ApplcationUser ApplcationUser { get; set; }
        public virtual List<ProdectSeller> Prodects { get; set; }
        [DefaultValue("false")]
        public bool status { get; set; }
        [DefaultValue("false")]
        public Boolean IsDelete { get; set; }
    }
}
