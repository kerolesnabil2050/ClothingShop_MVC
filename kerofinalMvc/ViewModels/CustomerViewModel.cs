using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class CustomerViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string? Image { get; set; }
        public IFormFile Photo { get; set; }

    }
}
