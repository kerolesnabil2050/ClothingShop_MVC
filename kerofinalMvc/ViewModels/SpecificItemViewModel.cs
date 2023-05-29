namespace Project.ViewModels
{
    public class SpecificItemViewModel
    {
        public string Name { get; set; }
        public int ProductId { get; set; }

        public string Description { get; set; }
        public string Type { get; set; }


        public string Brand { get; set; }
        public int Price { get; set; }


        public string? Imge { get; set; }

        public int Quntity { get; set; }
        public string Size { get; set; }

        public string Color { get; set; }


        public IFormFile Photo { get; set; }


    }

}
