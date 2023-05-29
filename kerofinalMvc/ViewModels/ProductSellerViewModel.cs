using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class ProductSellerViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }

        public string Type { get; set; }
        [DisplayName("CategoryName")]
        public int CategoryId { get; set; }
        public string? SellerId { get; set; }
        public int Quntity { get;set; }
        public string Color { get; set; }
        public string Size { get; set; } 
        public string? Img { get; set; }
        public IFormFile Photo { get; set; }


    }
}
