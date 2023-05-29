using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class ShiperViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Range(minimum: 18, maximum: 60)]
        public int Age { get; set; }
        [MinLength(14), MaxLength(14)]
        public string SSD { get; set; }

        public string? LicenseImg { get; set; }

        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.Password)]
        [Compare("PassWord")]
        public string ConfirmPassWord { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
