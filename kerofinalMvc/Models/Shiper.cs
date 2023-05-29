using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Project.Models
{
    public class Shiper
    {
        [ForeignKey("ApplcationUser")]
        [Key]
        public string ApplcationUserId { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public string SSD { get; set; }
        public string LicenseImg { get; set; }

        public virtual ApplcationUser ApplcationUser { get; set; }
        public virtual List<Order> Orders { get; set; }
        [DefaultValue("false")]
        public bool status { get; set; }
        [DefaultValue("false")]


        public bool busy { get; set; }
        [DefaultValue("false")]

        public bool IsDelete { get; set; }
    }
}
