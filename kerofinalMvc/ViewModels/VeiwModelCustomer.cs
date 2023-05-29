using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class VeiwModelCustomer
    {
        [Key]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [RegularExpression("Male|Female")]
       
        public string Gender { get; set; }
        public string? image { get; set; }
        public IFormFile Photo { get; set; }

    }
}
