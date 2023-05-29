using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class SuplierRegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [DataType(DataType.Password)]
        [Compare("PassWord")]
        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }
        [Range(minimum:18,maximum:90)]
        public int Age { get; set; }
        [MinLength(14),MaxLength(14)]
        public string SSD { get; set; }

        public string? Img { get; set; }
        public IFormFile Photo { get; set; }


    }
}
