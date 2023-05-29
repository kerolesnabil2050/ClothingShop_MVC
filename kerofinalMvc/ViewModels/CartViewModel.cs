using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class CartViewModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        [Key]
        public int Quntity { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string? Img { get; set; }
        public string Price { get; set; }
        public IFormFile Photo { get; set; }

    }
}
