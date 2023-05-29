using System.ComponentModel;

namespace Project.Models
{
    public class prodect
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Brand { get;set; }
        public string Type { get; set; }    
        public int CategoryId { get; set; } 
        [DefaultValue("false")]
        public Boolean IsDelete { get; set; }
        public virtual List<ProdectSeller>? ProdectSeller { get;set; }
        public virtual Category? Category { get; set; }  
    }
}
